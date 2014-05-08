﻿using AutoMapper;
using FWCB2014.Domain.Core.Models;
using FWCB2014.Domain.Core.Models.Query.Groups;
using FWCB2014.Domain.Core.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FWCB2014.Syndication.Infrastructure.Repositories
{
    // todo: this class should be optimized
    // todo: inject country repository ???
    public class GroupsRepository : IRepository<GroupModel>
    {
        private readonly string _countriesJsonPath;
        private readonly string _teamCountryMappingJsonPath;
        private readonly string _standingsJsonPath;

        public GroupsRepository(string countriesJsonPath, string teamCountryMappingJsonPath, string standingsJsonPath)
        {
            _countriesJsonPath = countriesJsonPath;
            _teamCountryMappingJsonPath = teamCountryMappingJsonPath;
            _standingsJsonPath = standingsJsonPath;
        }

        public IEnumerable<GroupModel> Find(Func<GroupModel, bool> predicate)
        {
            var standingsJson = File.ReadAllText(_standingsJsonPath);
            var standings = JsonConvert.DeserializeObject<IEnumerable<StandingModel>>(standingsJson);

            var list = new List<GroupModel>();
            foreach (var @group in standings.GroupBy(e => e.GroupId))
            {
                var groupId = @group.First().GroupId;
                var model = new GroupModel { GroupId = groupId, Standings = GetStandings(@group), };
                list.Add(model);
            }
            return list.Where(predicate);
        }

        private IEnumerable<StandingModel> GetStandings(IEnumerable<StandingModel> standings)
        {
            var list = new List<StandingModel>();
            foreach (var standing in standings)
            {
                standing.Team = GetTeam(standing.TeamId);
                list.Add(standing);
            }
            return list;
        }

        private TeamModel GetTeam(string teamId)
        {
            var countriesJson = File.ReadAllText(_countriesJsonPath);
            var countries = JsonConvert.DeserializeObject<IEnumerable<CountryModel>>(countriesJson);

            var teamCountryMappingJson = File.ReadAllText(_teamCountryMappingJsonPath);
            var teamCountryMapping = JsonConvert.DeserializeObject<IEnumerable<KeyValueModel>>(teamCountryMappingJson);

            var value = teamCountryMapping.First(e => e.Key == teamId).Value;

            Mapper.CreateMap<CountryModel, TeamModel>();
            var team = Mapper.Map<TeamModel>(countries.First(e => e.Alpha3Code == value));
            team.TeamId = teamId;
            return team;
        }

        // info: WTF!
        private class KeyValueModel
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }
    }
}