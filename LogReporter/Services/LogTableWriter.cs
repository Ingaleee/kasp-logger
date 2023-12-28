using System.Data;

namespace ConsoleApp1.Services;

// TODO: make abstraction
public class TableBuilder
{
    private string _tableName;
    private readonly DataTable _table = new();

    public TableBuilder Name(string name)
    {
        _tableName = name;
        return this;
    }
    
    public TableBuilder AddColumn<TType>(string name)
    {
        _table.Columns.Add(new DataColumn(name, typeof(TType)));
        return this;
    }

    public void AddRow(Action<DataRow> modifier)
    {
        var newRow = _table.NewRow();
        modifier(newRow);
        _table.Rows.Add(newRow);
    }
    
    public DataTable GetCurrentBuild()
    {
        return _table.Copy();
    }
}