using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AllDeductedDatabaseImplement.Migrations
{
    public partial class InirCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    login = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    mail = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_customer_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "provider",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_provider", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_provider_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "thread",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    faculty = table.Column<string>(type: "text", nullable: false),
                    customer_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_thread", x => x.id);
                    table.ForeignKey(
                        name: "FK_thread_customer_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customer",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date_create = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    provider_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_provider_provider_id",
                        column: x => x.provider_id,
                        principalTable: "provider",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "discipline",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    hours_count = table.Column<int>(type: "integer", nullable: false),
                    thread_id = table.Column<int>(type: "integer", nullable: false),
                    customer_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_discipline", x => x.id);
                    table.ForeignKey(
                        name: "FK_discipline_customer_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customer",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_discipline_thread_thread_id",
                        column: x => x.thread_id,
                        principalTable: "thread",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "group",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    curator_name = table.Column<string>(type: "text", nullable: false),
                    thread_id = table.Column<int>(type: "integer", nullable: false),
                    customer_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group", x => x.id);
                    table.ForeignKey(
                        name: "FK_group_customer_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customer",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_group_thread_thread_id",
                        column: x => x.thread_id,
                        principalTable: "thread",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "student",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    patronymic = table.Column<string>(type: "text", nullable: false),
                    thread_id = table.Column<int>(type: "integer", nullable: false),
                    provider_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student", x => x.id);
                    table.ForeignKey(
                        name: "FK_student_provider_provider_id",
                        column: x => x.provider_id,
                        principalTable: "provider",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_student_thread_thread_id",
                        column: x => x.thread_id,
                        principalTable: "thread",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_group",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_id = table.Column<int>(type: "integer", nullable: false),
                    group_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_group", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_group_group_group_id",
                        column: x => x.group_id,
                        principalTable: "group",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_group_order_order_id",
                        column: x => x.order_id,
                        principalTable: "order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_student",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_id = table.Column<int>(type: "integer", nullable: false),
                    student_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_student", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_student_order_order_id",
                        column: x => x.order_id,
                        principalTable: "order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_student_student_student_id",
                        column: x => x.student_id,
                        principalTable: "student",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "studying_status",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    student_id = table.Column<int>(type: "integer", nullable: false),
                    studying_form = table.Column<int>(type: "integer", nullable: false),
                    studying_base = table.Column<int>(type: "integer", nullable: false),
                    course = table.Column<int>(type: "integer", nullable: false),
                    provider_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_studying_status", x => x.id);
                    table.ForeignKey(
                        name: "FK_studying_status_provider_provider_id",
                        column: x => x.provider_id,
                        principalTable: "provider",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_studying_status_student_student_id",
                        column: x => x.student_id,
                        principalTable: "student",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_discipline_customer_id",
                table: "discipline",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_discipline_thread_id",
                table: "discipline",
                column: "thread_id");

            migrationBuilder.CreateIndex(
                name: "IX_group_customer_id",
                table: "group",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_group_thread_id",
                table: "group",
                column: "thread_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_provider_id",
                table: "order",
                column: "provider_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_group_group_id",
                table: "order_group",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_group_order_id",
                table: "order_group",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_student_order_id",
                table: "order_student",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_student_student_id",
                table: "order_student",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_student_provider_id",
                table: "student",
                column: "provider_id");

            migrationBuilder.CreateIndex(
                name: "IX_student_thread_id",
                table: "student",
                column: "thread_id");

            migrationBuilder.CreateIndex(
                name: "IX_studying_status_provider_id",
                table: "studying_status",
                column: "provider_id");

            migrationBuilder.CreateIndex(
                name: "IX_studying_status_student_id",
                table: "studying_status",
                column: "student_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_thread_customer_id",
                table: "thread",
                column: "customer_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "discipline");

            migrationBuilder.DropTable(
                name: "order_group");

            migrationBuilder.DropTable(
                name: "order_student");

            migrationBuilder.DropTable(
                name: "studying_status");

            migrationBuilder.DropTable(
                name: "group");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "student");

            migrationBuilder.DropTable(
                name: "provider");

            migrationBuilder.DropTable(
                name: "thread");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
