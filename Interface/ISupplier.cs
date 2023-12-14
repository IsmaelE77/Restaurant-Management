namespace Restaurant_Management.Interface;

public interface ISupplier : ICrud<Supplier>{
    public Supplier? GetSupplierWithHighestPaymentForYear(int year);
}
