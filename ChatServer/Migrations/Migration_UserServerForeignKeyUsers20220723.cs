using System;
using FluentMigrator;

namespace ChatServer.Migrations
{
    [Migration(20220723150100)]
    public class Migration_UserServerForeignKeyUsers20220723 : Migration
    {
        public override void Down()
        {
            Delete.ForeignKey();
        }

        public override void Up()
        {
           Create.ForeignKey()
                    .FromTable("user_server").ForeignColumn("user")
                    .ToTable("users").PrimaryColumn("id");
        }
    }
}
