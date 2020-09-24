using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Business.Utilities
{
    public static class ExpressionUtility 
    {
        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            // build parameter map (from parameters of second to parameters of first)
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with parameters from the first
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            // apply composition of lambda expression bodies to parameters from the first expression 
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.And);
        }
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.Or);
        }

        public static Expression<Func<T, bool>> WhereStringEqual<T>(string propertyName, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "type");
            var propertyExpression = Expression.Property(parameter, propertyName);
            MethodInfo method = typeof(StringExtension).GetMethod("StringEqual", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            var valueExpression = Expression.Constant(value);
            var containsExpression = Expression.Call(method, propertyExpression, valueExpression);
            return Expression.Lambda<Func<T, bool>>(containsExpression, parameter);
        }
        public static Expression<Func<T, bool>> WhereStringNotEqual<T>(string propertyName, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "type");
            var propertyExpression = Expression.Property(parameter, propertyName);
            MethodInfo method = typeof(StringExtension).GetMethod("StringNotEqual", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            var valueExpression = Expression.Constant(value);
            var containsExpression = Expression.Call(method, propertyExpression, valueExpression);
            return Expression.Lambda<Func<T, bool>>(containsExpression, parameter);
        }

        public static Expression<Func<T, bool>> WhereStringIsNull<T>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T), "type");
            var propertyExpression = Expression.Property(parameter, propertyName);
            MethodInfo method = typeof(StringExtension).GetMethod("StringIsNull", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            //var valueExpression = Expression.Constant(value);
            var containsExpression = Expression.Call(method, propertyExpression);
            return Expression.Lambda<Func<T, bool>>(containsExpression, parameter);
        }

        public static Expression<Func<T, bool>> WhereStringIsNotNull<T>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T), "type");
            var propertyExpression = Expression.Property(parameter, propertyName);
            MethodInfo method = typeof(StringExtension).GetMethod("StringIsNotNull", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            //var valueExpression = Expression.Constant(value);
            var containsExpression = Expression.Call(method, propertyExpression);
            return Expression.Lambda<Func<T, bool>>(containsExpression, parameter);
        }

        public static Expression<Func<T, bool>> WhereStringLessThan<T>(string propertyName, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "type");
            var propertyExpression = Expression.Property(parameter, propertyName);
            MethodInfo method = typeof(StringExtension).GetMethod("StringLessThan", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            var valueExpression = Expression.Constant(value);
            var containsExpression = Expression.Call(method, propertyExpression, valueExpression);
            return Expression.Lambda<Func<T, bool>>(containsExpression, parameter);
        }

        public static Expression<Func<T, bool>> WhereStringLessThanOrEqual<T>(string propertyName, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "type");
            var propertyExpression = Expression.Property(parameter, propertyName);
            MethodInfo method = typeof(StringExtension).GetMethod("StringLessThanOrEqual", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            var valueExpression = Expression.Constant(value);
            var containsExpression = Expression.Call(method, propertyExpression, valueExpression);
            return Expression.Lambda<Func<T, bool>>(containsExpression, parameter);
        }

        public static Expression<Func<T, bool>> WhereStringGreaterThan<T>(string propertyName, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "type");
            var propertyExpression = Expression.Property(parameter, propertyName);
            MethodInfo method = typeof(StringExtension).GetMethod("StringGreaterThan", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            var valueExpression = Expression.Constant(value);
            var containsExpression = Expression.Call(method, propertyExpression, valueExpression);
            return Expression.Lambda<Func<T, bool>>(containsExpression, parameter);
        }

        public static Expression<Func<T, bool>> WhereStringGreaterThanOrEqual<T>(string propertyName, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "type");
            var propertyExpression = Expression.Property(parameter, propertyName);
            MethodInfo method = typeof(StringExtension).GetMethod("StringGreaterThanOrEqual", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            var valueExpression = Expression.Constant(value);
            var containsExpression = Expression.Call(method, propertyExpression, valueExpression);
            return Expression.Lambda<Func<T, bool>>(containsExpression, parameter);
        }
        public static Expression<Func<T, bool>> WhereStringStartsWith<T>(string propertyName, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "type");
            var propertyExpression = Expression.Property(parameter, propertyName);
            MethodInfo method = typeof(StringExtension).GetMethod("StringStartsWith", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            var valueExpression = Expression.Constant(value);
            var containsExpression = Expression.Call(method, propertyExpression, valueExpression);
            return Expression.Lambda<Func<T, bool>>(containsExpression, parameter);
        }
        public static Expression<Func<T, bool>> WhereStringEndsWith<T>(string propertyName, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "type");
            var propertyExpression = Expression.Property(parameter, propertyName);
            MethodInfo method = typeof(StringExtension).GetMethod("StringEndsWith", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            var valueExpression = Expression.Constant(value);
            var containsExpression = Expression.Call(method, propertyExpression, valueExpression);
            return Expression.Lambda<Func<T, bool>>(containsExpression, parameter);
        }
        public static Expression<Func<T, bool>> WhereStringContains<T>(string propertyName, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "type");
            var propertyExpression = Expression.Property(parameter, propertyName);
            MethodInfo method = typeof(StringExtension).GetMethod("StringContains", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            var valueExpression = Expression.Constant(value);
            var containsExpression = Expression.Call(method, propertyExpression, valueExpression);
            return Expression.Lambda<Func<T, bool>>(containsExpression, parameter);
        }
        public static Expression<Func<T, bool>> WhereStringNotContains<T>(string propertyName, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "type");
            var propertyExpression = Expression.Property(parameter, propertyName);
            MethodInfo method = typeof(StringExtension).GetMethod("StringNotContains", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            var valueExpression = Expression.Constant(value);
            var containsExpression = Expression.Call(method, propertyExpression, valueExpression);
            return Expression.Lambda<Func<T, bool>>(containsExpression, parameter);
        }
        public static Expression<Func<T, bool>> WhereStringEmpty<T>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T), "type");
            var propertyExpression = Expression.Property(parameter, propertyName);
            MethodInfo method = typeof(StringExtension).GetMethod("StringEmpty", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
   
            var containsExpression = Expression.Call(method, propertyExpression);
            return Expression.Lambda<Func<T, bool>>(containsExpression, parameter);
        }
        public static Expression<Func<T, bool>> WhereStringNotEmpty<T>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T), "type");
            var propertyExpression = Expression.Property(parameter, propertyName);
            MethodInfo method = typeof(StringExtension).GetMethod("StringNotEmpty", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            var containsExpression = Expression.Call(method, propertyExpression);
            return Expression.Lambda<Func<T, bool>>(containsExpression, parameter);
        }

        public static Expression<Func<T, bool>> WhereDateTimeEqual<T>(string propertyName, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "type");
            var propertyExpression = Expression.Property(parameter, propertyName);
            MethodInfo method = typeof(StringExtension).GetMethod("DateTimeEqual", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            var dateValue = CommonMethods.ConvertStringToDateTime(value);
            var valueExpression = Expression.Constant(dateValue);
            var containsExpression = Expression.Call(method, propertyExpression, valueExpression);
            return Expression.Lambda<Func<T, bool>>(containsExpression, parameter);
        }
        public static Expression<Func<T, bool>> WhereDateTimeNotEqual<T>(string propertyName, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "type");
            var propertyExpression = Expression.Property(parameter, propertyName);
            MethodInfo method = typeof(StringExtension).GetMethod("DateTimeNotEqual", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            var dateValue = CommonMethods.ConvertStringToDateTime(value);
            var valueExpression = Expression.Constant(dateValue);
            var containsExpression = Expression.Call(method, propertyExpression, valueExpression);
            return Expression.Lambda<Func<T, bool>>(containsExpression, parameter);
        }

        public static Expression<Func<T, bool>> WhereDateTimeIsNull<T>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T), "type");
            var propertyExpression = Expression.Property(parameter, propertyName);
            MethodInfo method = typeof(StringExtension).GetMethod("DateTimeIsNull", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            //var valueExpression = Expression.Constant(value);
            var containsExpression = Expression.Call(method, propertyExpression);
            return Expression.Lambda<Func<T, bool>>(containsExpression, parameter);
        }

        public static Expression<Func<T, bool>> WhereDateTimeIsNotNull<T>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T), "type");
            var propertyExpression = Expression.Property(parameter, propertyName);
            MethodInfo method = typeof(StringExtension).GetMethod("DateTimeIsNotNull", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            //var valueExpression = Expression.Constant(value);
            var containsExpression = Expression.Call(method, propertyExpression);
            return Expression.Lambda<Func<T, bool>>(containsExpression, parameter);
        }

        public static Expression<Func<T, bool>> WhereDateTimeLessThan<T>(string propertyName, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "type");
            var propertyExpression = Expression.Property(parameter, propertyName);
            MethodInfo method = typeof(StringExtension).GetMethod("DateTimeLessThan", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            var dateValue = CommonMethods.ConvertStringToDateTime(value);
            var valueExpression = Expression.Constant(dateValue);
            var containsExpression = Expression.Call(method, propertyExpression, valueExpression);
            return Expression.Lambda<Func<T, bool>>(containsExpression, parameter);
        }

        public static Expression<Func<T, bool>> WhereDateTimeLessThanOrEqual<T>(string propertyName, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "type");
            var propertyExpression = Expression.Property(parameter, propertyName);
            MethodInfo method = typeof(StringExtension).GetMethod("DateTimeLessThanOrEqual", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            var dateValue = CommonMethods.ConvertStringToDateTime(value);
            var valueExpression = Expression.Constant(dateValue);
            var containsExpression = Expression.Call(method, propertyExpression, valueExpression);
            return Expression.Lambda<Func<T, bool>>(containsExpression, parameter);
        }

        public static Expression<Func<T, bool>> WhereDateTimeGreaterThan<T>(string propertyName, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "type");
            var propertyExpression = Expression.Property(parameter, propertyName);
            MethodInfo method = typeof(StringExtension).GetMethod("DateTimeGreaterThan", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            var dateValue = CommonMethods.ConvertStringToDateTime(value);
            var valueExpression = Expression.Constant(dateValue);
            var containsExpression = Expression.Call(method, propertyExpression, valueExpression);
            return Expression.Lambda<Func<T, bool>>(containsExpression, parameter);
        }

        public static Expression<Func<T, bool>> WhereDateTimeGreaterThanOrEqual<T>(string propertyName, string value)
        {
            var parameter = Expression.Parameter(typeof(T), "type");
            var propertyExpression = Expression.Property(parameter, propertyName);
            MethodInfo method = typeof(StringExtension).GetMethod("DateTimeGreaterThanOrEqual", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            var dateValue = CommonMethods.ConvertStringToDateTime(value);
            var valueExpression = Expression.Constant(dateValue);
            var containsExpression = Expression.Call(method, propertyExpression, valueExpression);
            return Expression.Lambda<Func<T, bool>>(containsExpression, parameter);
        }
    }
}
