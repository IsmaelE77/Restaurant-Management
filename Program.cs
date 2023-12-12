using Moq;
using Restaurant_Management.Repository;
using System.Data;
using Xunit;

namespace Restaurant_Management;

public class Program
{
    private const string CONNECTION_STRING = "DATA SOURCE=localhost:1521/XEPDB1;PERSIST SECURITY INFO=True;USER ID=admin;Password=admin";

    public static void Main()
    {
        ////Database.CreateTables(GetConnection());
        //SectionRepository section = new(CONNECTION_STRING);
        //Console.WriteLine(section.GetId("Kit"));
        //foreach (var sec in section.GetAll())
        //{
        //    Console.WriteLine($"(Id: {sec?.Id}, Name: {sec?.Name})");
        //}
        // Arrange
        //Arrange
        var readerMock = new Mock<IDataReader>();

        var commandMock = new Mock<IDbCommand>();
        commandMock.Setup(m => m.ExecuteReader()).Returns(readerMock.Object).Verifiable();

        var parameterMock = new Mock<IDbDataParameter>();

        commandMock.Setup(m => m.CreateParameter()).Returns(parameterMock.Object);

        commandMock.Setup(m => m.Parameters.Add(It.IsAny<IDbDataParameter>())).Verifiable();

        var connectionMock = new Mock<IDbConnection>();
        connectionMock.Setup(m => m.CreateCommand()).Returns(commandMock.Object);

        var connectionFactoryMock = new Mock<IDbConnectionFactory>();
        connectionFactoryMock.Setup(m => m.CreateConnection()).Returns(connectionMock.Object);

        var sut = new MyDataAccessClass(connectionFactoryMock.Object);
        var input = "some value";

        //Act
        var data = sut.GetData(input);

        //Assert
        commandMock.Verify();
    }
    public static OracleConnection GetConnection()
    {
        return new OracleConnection(CONNECTION_STRING);
    }
}
