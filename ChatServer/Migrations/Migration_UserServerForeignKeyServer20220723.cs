using System;
using FluentMigrator;

namespace ChatServer.Migrations
{
    [Migration(20220723150400)]
    public class Migration_UserServerForeignKeyServer20220723 : Migration
    {
        public override void Down()
        {
            Delete.ForeignKey();
        }

        public override void Up()
        {
            Create.ForeignKey()
                   .FromTable("user_server").ForeignColumn("server")
                   .ToTable("server").PrimaryColumn("id");
        }
    }
}
