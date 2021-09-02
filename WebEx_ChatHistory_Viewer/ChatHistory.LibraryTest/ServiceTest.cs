using Service.Library;
using System;
using Xunit;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace ChatHistory.LibraryTest
{
    public class ServiceTest
    {
        [Fact]
        public void ValidateReadData()
        {

            //--Arrange
            Services _services = new Services(new MockJsonDataSource());

            string actual = @"[
    {
        "id": "Y2lzY29zcGFyazovL3VzL01FU1NBR0UvM2U3MzEwZjAtOGQ1Zi0xMWViLWE1NDYtNjM0OGFmODA1ZDhi",
        "roomId": "Y2lzY29zcGFyazovL3VzL1JPT00vNTA2ZjdmYTYtYjVmYS0zZjI3LWE3N2QtYzE4MDJkOGNhYjI4",
        "roomType": "direct",
        "files": [
            "https://webexapis.com/v1/contents/Y2lzY29zcGFyazovL3VzL0NPTlRFTlQvM2U3MzEwZjAtOGQ1Zi0xMWViLWE1NDYtNjM0OGFmODA1ZDhiLzA"
        ],
        "personId": "Y2lzY29zcGFyazovL3VzL1BFT1BMRS9hNTQwYzY2My04YjU0LTRlZmEtYjVjOS05ZDdkMjNlZDZmZWQ",
        "personEmail": "dhiren.goyal@klingelnberg.com",
        "created": "2021-03-25T11:42:57.407Z"
    },
    {
        "id": "Y2lzY29zcGFyazovL3VzL01FU1NBR0UvM2I3MGY0ODAtOGQ1Zi0xMWViLWJiNDctYzExYzRiOGU3ZWUy",
        "roomId": "Y2lzY29zcGFyazovL3VzL1JPT00vNTA2ZjdmYTYtYjVmYS0zZjI3LWE3N2QtYzE4MDJkOGNhYjI4",
        "roomType": "direct",
        "text": "so that partNo",
        "personId": "Y2lzY29zcGFyazovL3VzL1BFT1BMRS9mY2EyOWI4Mi1hYjYxLTQ0NTMtOWRjNS1mNjQ5YjEzOWY0ZGU",
        "personEmail": "Sanket.Naik@klingelnberg.com",
        "html": "<p>so that<b> partNo</b></p>",
        "created": "2021-03-25T11:42:52.360Z"
    },
    {
        "id": "Y2lzY29zcGFyazovL3VzL01FU1NBR0UvMzA1MjFmNzAtOGQ1Zi0xMWViLWI2ZGEtY2Y1YTlmM2ZmMjJh",
        "roomId": "Y2lzY29zcGFyazovL3VzL1JPT00vNTA2ZjdmYTYtYjVmYS0zZjI3LWE3N2QtYzE4MDJkOGNhYjI4",
        "roomType": "direct",
        "text": "\ud83d\ude02",
        "personId": "Y2lzY29zcGFyazovL3VzL1BFT1BMRS9mY2EyOWI4Mi1hYjYxLTQ0NTMtOWRjNS1mNjQ5YjEzOWY0ZGU",
        "personEmail": "Sanket.Naik@klingelnberg.com",
        "html": "<h1><p>\ud83d\ude02</p></h1>",
        "created": "2021-03-25T11:42:33.703Z"
    },
    {
        "id": "Y2lzY29zcGFyazovL3VzL01FU1NBR0UvMmRiYzgwNzAtOGQ1Zi0xMWViLWFkODMtYmQ0MjE1YmM5NDY2",
        "roomId": "Y2lzY29zcGFyazovL3VzL1JPT00vNTA2ZjdmYTYtYjVmYS0zZjI3LWE3N2QtYzE4MDJkOGNhYjI4",
        "roomType": "direct",
        "text": "partNumber",
        "personId": "Y2lzY29zcGFyazovL3VzL1BFT1BMRS9mY2EyOWI4Mi1hYjYxLTQ0NTMtOWRjNS1mNjQ5YjEzOWY0ZGU",
        "personEmail": "Sanket.Naik@klingelnberg.com",
        "html": "<p><b>partNumber</b></p>",
        "created": "2021-03-25T11:42:29.367Z"
    },
    {
        "id": "Y2lzY29zcGFyazovL3VzL01FU1NBR0UvZjlkN2U1MTAtOGQ1ZS0xMWViLTgyOWItNmQzYjIzOWI0M2Nj",
        "roomId": "Y2lzY29zcGFyazovL3VzL1JPT00vNTA2ZjdmYTYtYjVmYS0zZjI3LWE3N2QtYzE4MDJkOGNhYjI4",
        "roomType": "direct",
        "text": "MachineNo use karte he na ham ST me ?",
        "personId": "Y2lzY29zcGFyazovL3VzL1BFT1BMRS9mY2EyOWI4Mi1hYjYxLTQ0NTMtOWRjNS1mNjQ5YjEzOWY0ZGU",
        "personEmail": "Sanket.Naik@klingelnberg.com",
        "created": "2021-03-25T11:41:02.305Z"
    }
]";


    //--Act

    var result = _services.ReadUserChatData(@"F:\PProject\WPF\WebexDump\MockDirectChats\Dhiren Goyal\messages.json");


            var x = JsonConvert.DeserializeObject<string>(result);

            //--Assert
            Assert.Equal(actual, x);


        }
    }
}
