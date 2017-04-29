using ExcelToSQLScripts;
using ExcelToSQLScripts.Models;
using FluentAssertions;
using Xunit;
using Record = ExcelToSQLScripts.Models.Record;

namespace Tests
{
    public class QueryMakerTests
    {
        [Fact]
        public void CanMakeQuery()
        {
            QueryMaker queryMaker = new QueryMaker(new ValueRenderer(new string[]{ }));

            Record record = Utils.GetTable().Records[0];

            string query = queryMaker.GenerateQuery(record);

            query.Should().Be("INSERT INTO EMPLOYEES (ID, NAME) VALUES (1, 'bilal');\n");
        }

        [Fact]
        public void CanReplaceSingleQuoteWithDoubleQuote()
        {
            QueryMaker queryMaker = new QueryMaker(new ValueRenderer(new string[] { }));

            Record record = Utils.GetTable(name:"sky's blue").Records[0];

            string query = queryMaker.GenerateQuery(record);

            query.Should().Be("INSERT INTO EMPLOYEES (ID, NAME) VALUES (1, 'sky''s blue');\n");
        }

        [Fact]
        public void CanReplaceNullReplacementsWithNulls()
        {
            QueryMaker queryMaker = new QueryMaker(new ValueRenderer(new [] { "n/a" }));

            Record record = Utils.GetTable(name: "N/A").Records[0];

            string query = queryMaker.GenerateQuery(record);

            query.Should().Be("INSERT INTO EMPLOYEES (ID, NAME) VALUES (1, NULL);\n");
        }
    }
}