using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace ApropasTaskManager.Server;


public class SearchOptionsBuilder<T>
{
    private static readonly MethodInfo ContainsStringMethod = typeof(string).GetMethod("Contains",
        BindingFlags.Public | BindingFlags.Instance, 
        new[] { typeof(string), typeof(StringComparison) }
    )!; 

    private readonly ParameterExpression _parameter;
    private readonly List<Expression> _binaryExpressions = new();

    public SearchOptionsBuilder()
    {
        _parameter = Expression.Parameter(typeof(T), "x");
    }

    public SearchOptionsBuilder<T> Or<TProperty>(Expression<Func<T, TProperty>> expression, params TProperty[] values)
    {
        if (values.Length == 0)
        {
            return this;
        }

        var propertyInfo = SearchOptionsBuilder<T>.ThrowIfNotProperty(expression);

        if (values.Length == 0)
        {
            throw new ArgumentException("Values cannot be empty", nameof(values));
        }

        var property = Expression.Property(_parameter, propertyInfo);

        var finalBinary = SearchOptionsBuilder<T>.IsInArray(values, property);

        _binaryExpressions.Add(finalBinary);

        return this;
    }

    public SearchOptionsBuilder<T> Or(Expression<Func<T, string>> expression, params string[] values)
    {
        if (values.Length == 0)
        {
            return this;
        }

        var propertyInfo = SearchOptionsBuilder<T>.ThrowIfNotProperty(expression);

        if (values.Length == 0)
        {
            throw new ArgumentException("Values cannot be empty", nameof(values));
        }

        var property = Expression.Property(_parameter, propertyInfo);

        var finalBinary = SearchOptionsBuilder<T>.IsInArray(values, property);

        _binaryExpressions.Add(finalBinary);

        return this;
    }



    public Expression<Func<T, bool>> BuildExpression()
    {
        var finalBinary = _binaryExpressions[0];
        for (var i = 1; i < _binaryExpressions.Count; i++)
        {
            finalBinary = Expression.OrElse(finalBinary, _binaryExpressions[i]);
        }
        return Expression.Lambda<Func<T, bool>>(finalBinary, _parameter);
    }

    private static PropertyInfo ThrowIfNotProperty<TProperty>(Expression<Func<T, TProperty>> expression)
    {
        if (expression.Body is not MemberExpression member || member.Member is not PropertyInfo propertyInfo)
        {
            throw new ArgumentException("Need reference to property", nameof(expression));
        }

        return propertyInfo;
    }

    private static Expression IsInArray<TProperty>(TProperty[] values, MemberExpression property)
    {
        var finalBinary = Expression.Equal(property, Expression.Constant(values[0]));
        for (var i = 1; i < values.Length; i++)
        {
            var equals = Expression.Equal(property, Expression.Constant(values[i]));
            finalBinary = Expression.OrElse(finalBinary, equals);
        }
        return finalBinary;
    }

    private static Expression IsInArray(string[] values, MemberExpression property)
    {
        var contains = Expression.Call(
                property,
                ContainsStringMethod,
                Expression.Constant(values[0]),
                Expression.Constant(StringComparison.InvariantCultureIgnoreCase)
        );

        Expression finalBinary = contains;

        values[0].Contains("sad", StringComparison.InvariantCultureIgnoreCase);
        for (var i = 1; i < values.Length; i++)
        {
            contains = Expression.Call(
                property,
                ContainsStringMethod,
                Expression.Constant(values[i]),
                Expression.Constant(StringComparison.InvariantCultureIgnoreCase)
            );

            finalBinary = Expression.OrElse(finalBinary, contains);
        }
        return finalBinary;
    }
}
