using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rewards_Campaigns_CampaignId",
                table: "Rewards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rewards",
                table: "Rewards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Campaigns",
                table: "Campaigns");

            migrationBuilder.RenameTable(
                name: "Rewards",
                newName: "Reward");

            migrationBuilder.RenameTable(
                name: "Campaigns",
                newName: "Campaign");

            migrationBuilder.RenameIndex(
                name: "IX_Rewards_CampaignId",
                table: "Reward",
                newName: "IX_Reward_CampaignId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Reward",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Campaign",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reward",
                table: "Reward",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Campaign",
                table: "Campaign",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reward_Campaign_CampaignId",
                table: "Reward",
                column: "CampaignId",
                principalTable: "Campaign",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reward_Campaign_CampaignId",
                table: "Reward");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reward",
                table: "Reward");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Campaign",
                table: "Campaign");

            migrationBuilder.RenameTable(
                name: "Reward",
                newName: "Rewards");

            migrationBuilder.RenameTable(
                name: "Campaign",
                newName: "Campaigns");

            migrationBuilder.RenameIndex(
                name: "IX_Reward_CampaignId",
                table: "Rewards",
                newName: "IX_Rewards_CampaignId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Rewards",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Campaigns",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rewards",
                table: "Rewards",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Campaigns",
                table: "Campaigns",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rewards_Campaigns_CampaignId",
                table: "Rewards",
                column: "CampaignId",
                principalTable: "Campaigns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
