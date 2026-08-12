using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Executors.Action.Database;
using Application.Executors.Action.Database.Visitor.Operators;

namespace GFlowApp.Services.Schemas.Providers
{
    public class SqlProvider : INodeSchemaProvider
    {
        public string NodeType => "sql";
        private const string QueryConditionRefName = "QueryCondition";
        private static List<PropertyVariant> BuildQueryConditionVariants()
        {
            return new List<PropertyVariant>
    {
        new PropertyVariant
        {
            Name = "comparison",
            Properties = new List<PropertySchema>
            {
                new PropertySchema { Type = "string", Name = "Field", IsRequired = true },
                new PropertySchema { Type = "any", Name = "Value", IsRequired = true },
                new PropertySchema
                {
                    Type = "enum",
                    Name = "Operator",
                    IsRequired = true,
                    EnumValues = Enum.GetNames<ComparisonOperator>().ToList()
                }
            }
        },
        new PropertyVariant
        {
            Name = "group",
            Properties = new List<PropertySchema>
            {
                new PropertySchema
                {
                    Type = "enum",
                    Name = "Operator",
                    IsRequired = true,
                    EnumValues = Enum.GetNames<LogicalOperator>().ToList()
                },
                new PropertySchema
                {
                    Type = "array",
                    Name = "Conditions",
                    IsRequired = true,
                    ElementSchema = new PropertySchema
                    {
                        Type = "ref",
                        Name = "Condition",
                        IsRequired = true,
                        Ref = QueryConditionRefName
                    }
                }
            }
        },
        new PropertyVariant
        {
            Name = "not",
            Properties = new List<PropertySchema>
            {
                new PropertySchema
                {
                    Type = "ref",
                    Name = "Inner",
                    IsRequired = true,
                    Ref = QueryConditionRefName
                }
            }
        }
    };
        }
        public NodeTypeSchema GetSchema()
        {
            var whereRef = new PropertySchema
            {
                Type = "ref",
                Name = "Where",
                Ref = QueryConditionRefName
            };

            var schema = new NodeTypeSchema
            {
                Properties =
        new List<PropertySchema>
     {


         new PropertySchema
         {
             Type = "polymorphic",
             Name = "Definition",
             IsRequired = true,

             Descriminator = "operation",


             Properties = new List<PropertySchema>
             {
                 new PropertySchema
                 {
                     Type = "string",
                     Name = "TableName",
                     IsRequired = true
                 }
             },

             Variants = new List<PropertyVariant>
             {


                 new PropertyVariant
                 {
                     Name = "Select",

                     Properties = new List<PropertySchema>
                     {
                         new PropertySchema
                         {
                             Type = "array",
                             Name = "Columns",
                             IsRequired = true,

                             ElementSchema = new PropertySchema
                             {
                                 Type = "string",
                                 Name = "Column",
                                 IsRequired = true
                             }
                         },

                         new PropertySchema
                         {
                             Type = "polymorphic",
                             Name = "Where",
                             IsRequired = false,

                             Descriminator = "type",

                             Variants = BuildQueryConditionVariants()
                         }
                     }
                 },



                 new PropertyVariant
                 {
                     Name = "Insert",

                     Properties = new List<PropertySchema>
                     {
                         new PropertySchema
                         {
                             Type = "dictionary",
                             Name = "Values",
                             IsRequired = true,

                             ElementSchema = new PropertySchema
                             {
                                 Type = "any",
                                 Name = "Value"
                             }
                         }
                     }
                 },



                 new PropertyVariant
                 {
                     Name = "Update",

                     Properties = new List<PropertySchema>
                     {
                         new PropertySchema
                         {
                             Type = "dictionary",
                             Name = "Values",
                             IsRequired = true,

                             ElementSchema = new PropertySchema
                             {
                                 Type = "any",
                                 Name = "Value"
                             }
                         },

                         new PropertySchema
                         {
                             Type = "polymorphic",
                             Name = "Where",
                             IsRequired = true,

                             Descriminator = "type",

                             Variants = BuildQueryConditionVariants()
                         }
                     }
                 },



                 new PropertyVariant
                 {
                     Name = "Delete",

                     Properties = new List<PropertySchema>
                     {
                         new PropertySchema
                         {
                             Type = "polymorphic",
                             Name = "Where",
                             IsRequired = true,

                             Descriminator = "type",

                             Variants = BuildQueryConditionVariants()
                         }
                     }
                 },



                 new PropertyVariant
                 {
                     Name = "Raw",

                     Properties = new List<PropertySchema>
                     {
                         new PropertySchema
                         {
                             Type = "string",
                             Name = "Query",
                             IsRequired = true
                         },

                         new PropertySchema
                         {
                             Type = "dictionary",
                             Name = "Parameters",
                             IsRequired = false,

                             ElementSchema = new PropertySchema
                             {
                                 Type = "any",
                                 Name = "Value"
                             }
                         }
                     }
                 }
             }
         },



         new PropertySchema
         {
             Type = "object",
             Name = "SqlConnection",
             IsRequired = true,

             Properties = new List<PropertySchema>
             {


                 new PropertySchema
                 {
                     Type = "enum",
                     Name = "DatabaseType",
                     IsRequired = true,

                     EnumValues = Enum
                        .GetNames<DatabaseType>()
                        .ToList()
                 },

                 // ConnectionParameters

                 new PropertySchema
                 {
                     Type = "object",
                     Name = "ConnectionParameters",
                     IsRequired = true,

                     Properties = new List<PropertySchema>
                     {
                         new PropertySchema
                         {
                             Type = "string",
                             Name = "Host",
                             IsRequired = true
                         },

                         new PropertySchema
                         {
                             Type = "number",
                             Name = "Port",
                             IsRequired = false
                         },

                         new PropertySchema
                         {
                             Type = "string",
                             Name = "Username",
                             IsRequired = true
                         },

                         new PropertySchema
                         {
                             Type = "string",
                             Name = "Password",
                             IsRequired = true
                         },

                         new PropertySchema
                         {
                             Type = "string",
                             Name = "Database",
                             IsRequired = true
                         },

                         new PropertySchema
                         {
                             Type = "number",
                             Name = "TimeOut",
                             IsRequired = false
                         },

                         new PropertySchema
                         {
                             Type = "boolean",
                             Name = "Pooling",
                             IsRequired = false
                         },

                         new PropertySchema
                         {
                             Type = "enum",
                             Name = "SslMode",
                             IsRequired = true,

                             EnumValues = Enum
                                .GetNames<SslMode>()
                                .ToList()
                         },

                         new PropertySchema
                         {
                             Type = "number",
                             Name = "MinPoolSize",
                             IsRequired = false
                         },

                         new PropertySchema
                         {
                             Type = "number",
                             Name = "MaxPoolSize",
                             IsRequired = false
                         },

                         new PropertySchema
                         {
                             Type = "string",
                             Name = "SearchPath",
                             IsRequired = false
                         },

                         new PropertySchema
                         {
                             Type = "boolean",
                             Name = "AllowUserVariables",
                             IsRequired = false
                         },

                         new PropertySchema
                         {
                             Type = "string",
                             Name = "CharacterSet",
                             IsRequired = true
                         }
                     }
                 }
             }
         }
     }
            };

            schema.Definition[QueryConditionRefName] = BuildQueryConditionVariants();

            return schema;
         }
 
    } 
    
}
            