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
       
    
        public NodeTypeSchema GetSchema()
        {
            
            var schema = new NodeTypeSchema
            {
                Properties =
        new List<PropertySchema>
        {

        new PropertySchema
        {
            Type = "object",
            Name = "SqlConfiguration",
            IsRequired = true,
Properties = new List<PropertySchema>
{
     new PropertySchema
         {
             Type = "object",
             Name = "Definition",
             IsRequired = true,

             Descriminator = "operation",


             Properties = new List<PropertySchema>
             {
                
                    new PropertySchema
                 {
                     Name = "Raw",
                     Type="object",
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
             },
 
         }
        }
        }
        ,



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

           

            return schema;
         }
 
    } 
    
}
            