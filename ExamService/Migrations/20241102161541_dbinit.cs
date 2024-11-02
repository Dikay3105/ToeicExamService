using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExamService.Migrations
{
    /// <inheritdoc />
    public partial class dbinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Difficulty = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "history",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    TestID = table.Column<int>(type: "int", nullable: false),
                    Total_Listening = table.Column<int>(type: "int", nullable: false),
                    Total_Reading = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_history", x => x.Id);
                    table.ForeignKey(
                        name: "FK_history_Tests_TestID",
                        column: x => x.TestID,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TestID = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parts_Tests_TestID",
                        column: x => x.TestID,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "historydetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PartID = table.Column<int>(type: "int", nullable: false),
                    HistoryID = table.Column<int>(type: "int", nullable: false),
                    TotalQuestion = table.Column<int>(type: "int", nullable: false),
                    TotalCorrect = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_historydetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_historydetail_Parts_PartID",
                        column: x => x.PartID,
                        principalTable: "Parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_historydetail_history_HistoryID",
                        column: x => x.HistoryID,
                        principalTable: "history",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PartID = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImagePath = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AudioPath = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImageName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AudioName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AnswerCounts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Parts_PartID",
                        column: x => x.PartID,
                        principalTable: "Parts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    QuestionID = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsCorrect = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    QuestionID = table.Column<int>(type: "int", nullable: false),
                    SelectedAnswerID = table.Column<int>(type: "int", nullable: false),
                    HistoryID = table.Column<int>(type: "int", nullable: false),
                    IsCorrect = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAnswers_Answers_SelectedAnswerID",
                        column: x => x.SelectedAnswerID,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAnswers_Questions_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAnswers_history_HistoryID",
                        column: x => x.HistoryID,
                        principalTable: "history",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Tests",
                columns: new[] { "Id", "CreatedAt", "Description", "Difficulty", "Duration", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 2, 23, 15, 41, 495, DateTimeKind.Local).AddTicks(3821), "Mô tả bài kiểm tra 1", "Easy", 60, "Test 1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2024, 11, 2, 23, 15, 41, 495, DateTimeKind.Local).AddTicks(3837), "Mô tả bài kiểm tra 2", "Medium", 75, "Test 2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2024, 11, 2, 23, 15, 41, 495, DateTimeKind.Local).AddTicks(3839), "Mô tả bài kiểm tra 3", "Hard", 90, "Test 3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Parts",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "Number", "TestID", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 2, 23, 15, 41, 495, DateTimeKind.Local).AddTicks(3986), "Listening Comprehension", "Part 1", 1, 1, new DateTime(2024, 11, 2, 23, 15, 41, 495, DateTimeKind.Local).AddTicks(3987) },
                    { 2, new DateTime(2024, 11, 2, 23, 15, 41, 495, DateTimeKind.Local).AddTicks(3989), "Reading Comprehension", "Part 2", 2, 1, new DateTime(2024, 11, 2, 23, 15, 41, 495, DateTimeKind.Local).AddTicks(3989) },
                    { 3, new DateTime(2024, 11, 2, 23, 15, 41, 495, DateTimeKind.Local).AddTicks(3990), "Listening Comprehension", "Part 1", 1, 2, new DateTime(2024, 11, 2, 23, 15, 41, 495, DateTimeKind.Local).AddTicks(3991) },
                    { 4, new DateTime(2024, 11, 2, 23, 15, 41, 495, DateTimeKind.Local).AddTicks(4016), "Reading Comprehension", "Part 2", 2, 2, new DateTime(2024, 11, 2, 23, 15, 41, 495, DateTimeKind.Local).AddTicks(4016) }
                });

            migrationBuilder.InsertData(
                table: "history",
                columns: new[] { "Id", "EndTime", "StartTime", "TestID", "Total_Listening", "Total_Reading", "UserID" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 2, 23, 15, 41, 495, DateTimeKind.Local).AddTicks(4091), new DateTime(2024, 11, 2, 21, 15, 41, 495, DateTimeKind.Local).AddTicks(4086), 1, 75, 80, 1 },
                    { 2, new DateTime(2024, 11, 2, 23, 15, 41, 495, DateTimeKind.Local).AddTicks(4094), new DateTime(2024, 11, 2, 21, 15, 41, 495, DateTimeKind.Local).AddTicks(4093), 2, 85, 90, 2 }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "AnswerCounts", "AudioName", "AudioPath", "CreatedAt", "ImageName", "ImagePath", "PartID", "Text", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 4, null, null, new DateTime(2024, 11, 2, 23, 15, 41, 495, DateTimeKind.Local).AddTicks(4040), null, null, 1, "What is he doing?", new DateTime(2024, 11, 2, 23, 15, 41, 495, DateTimeKind.Local).AddTicks(4040) },
                    { 2, 4, null, null, new DateTime(2024, 11, 2, 23, 15, 41, 495, DateTimeKind.Local).AddTicks(4042), null, null, 1, "Where is he going?", new DateTime(2024, 11, 2, 23, 15, 41, 495, DateTimeKind.Local).AddTicks(4043) },
                    { 3, 4, null, null, new DateTime(2024, 11, 2, 23, 15, 41, 495, DateTimeKind.Local).AddTicks(4044), null, null, 2, "What does the sign say?", new DateTime(2024, 11, 2, 23, 15, 41, 495, DateTimeKind.Local).AddTicks(4045) },
                    { 4, 4, null, null, new DateTime(2024, 11, 2, 23, 15, 41, 495, DateTimeKind.Local).AddTicks(4046), null, null, 2, "What is the main idea of the passage?", new DateTime(2024, 11, 2, 23, 15, 41, 495, DateTimeKind.Local).AddTicks(4046) }
                });

            migrationBuilder.InsertData(
                table: "historydetail",
                columns: new[] { "Id", "HistoryID", "PartID", "TotalCorrect", "TotalQuestion" },
                values: new object[,]
                {
                    { 1, 1, 1, 8, 10 },
                    { 2, 1, 2, 7, 10 },
                    { 3, 2, 3, 9, 10 },
                    { 4, 2, 4, 6, 10 }
                });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "IsCorrect", "QuestionID", "Text" },
                values: new object[,]
                {
                    { 1, false, 1, "He is eating" },
                    { 2, true, 1, "He is talking on the phone" },
                    { 3, false, 1, "He is walking" },
                    { 4, false, 1, "He is reading" },
                    { 5, true, 2, "In a conference room" },
                    { 6, false, 2, "In a cafe" },
                    { 7, false, 2, "In a park" },
                    { 8, false, 2, "At the airport" }
                });

            migrationBuilder.InsertData(
                table: "UserAnswers",
                columns: new[] { "Id", "HistoryID", "IsCorrect", "QuestionID", "SelectedAnswerID", "UserID" },
                values: new object[,]
                {
                    { 3, 2, true, 3, 11, 2 },
                    { 4, 2, true, 4, 15, 2 },
                    { 1, 1, true, 1, 2, 1 },
                    { 2, 1, true, 2, 5, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionID",
                table: "Answers",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_history_TestID",
                table: "history",
                column: "TestID");

            migrationBuilder.CreateIndex(
                name: "IX_historydetail_HistoryID",
                table: "historydetail",
                column: "HistoryID");

            migrationBuilder.CreateIndex(
                name: "IX_historydetail_PartID",
                table: "historydetail",
                column: "PartID");

            migrationBuilder.CreateIndex(
                name: "IX_Parts_TestID",
                table: "Parts",
                column: "TestID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_PartID",
                table: "Questions",
                column: "PartID");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_HistoryID",
                table: "UserAnswers",
                column: "HistoryID");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_QuestionID",
                table: "UserAnswers",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_SelectedAnswerID",
                table: "UserAnswers",
                column: "SelectedAnswerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "historydetail");

            migrationBuilder.DropTable(
                name: "UserAnswers");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "history");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "Tests");
        }
    }
}
