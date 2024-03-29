﻿using System;
using System.Data;

namespace Test.Framework.DataAccess
{
    public interface IUpdateable<T> : IConvertable<T>
    {
        string UpdateSql();
        void ApplyUpdate(T instance, IDbCommand command);
    }
}
