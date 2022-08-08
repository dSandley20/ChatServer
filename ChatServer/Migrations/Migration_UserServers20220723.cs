using System;
using FluentMigrator;
using FluentMigrator.SqlServer;

namespace ChatServer.Migrations
{
    [Migration(3)]
    public class Migration_UserServers20220723 : Migration
    {
        public override void Down()
        {
            Delete.Table("user_server");
        }

        public override void Up()
        {
            Create.Table("user_server")
                .WithColumn("id").AsInt32().NotNullable().PrimaryKey().Identity(1, 1)
                .WithColumn("user").AsInt32().NotNullable()
                .WithColumn("server").AsInt32().NotNullable();
                 
        }
    }
}
