using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FwksLabs.Libs.Infra.Postgres.Extensions;

public static class TypeConfigurationExtension
{
    public static ReferenceCollectionBuilder HasForeignKey<TPrincipal, TDependent>(
        this ReferenceCollectionBuilder<TPrincipal, TDependent> builder,
        Expression<Func<TDependent, object?>> expression, string tableName)
            where TPrincipal : class
            where TDependent : class
    {
        string? propertyName;

        if (expression.Body is UnaryExpression unaryExpression)
            propertyName = (unaryExpression.Operand as MemberExpression)?.Member.Name;
        else if (expression.Body is MemberExpression memberExpression)
            propertyName = memberExpression.Member.Name;
        else
            throw new ArgumentException("Invalid expression type.");

        return builder
            .HasForeignKey(expression)
            .HasConstraintName($"FK_{tableName}_{propertyName}")
            .OnDelete(DeleteBehavior.Restrict);
    }
}