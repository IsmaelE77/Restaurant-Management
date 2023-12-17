namespace Restaurant_Management.Interface;

public interface IReceipt : ICrud<Receipt>
{
    public IEnumerable<Receipt> GetReceiptByTable(int Table_Id);
    public IEnumerable<Receipt> GetReceiptByBetween2Dates(DateTime from, DateTime to);
}