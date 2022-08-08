using FluentMigrator;

namespace ChatServer.Migrations
{
    [Migration(13)]
    public class Migration_UniqueUserLocationUser20220808 : Migration
    {
        public override void Down()
        {
            Delete.UniqueConstraint("user_location_unique_user");
        }

        public override void Up()
        {
            Create
                .UniqueConstraint("user_location_unique_user")
                .OnTable("user_location")
                .Column("user");
        }
    }
}
