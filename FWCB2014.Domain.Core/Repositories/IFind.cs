﻿using System;
using System.Collections.Generic;

namespace FWCB2014.Domain.Core.Repositories
{
  public interface IFind<out T>
  {
    IEnumerable<T> Find(Func<T, bool> predicate);
  }
}