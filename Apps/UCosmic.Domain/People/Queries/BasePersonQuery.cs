﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UCosmic.Domain.People
{
    public class BasePersonQuery
    {
        public IEnumerable<Expression<Func<Person, object>>> EagerLoad { get; set; }
    }
}