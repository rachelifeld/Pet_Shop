namespace DO;
//nnn
public struct Order
{
    public int orderId { get; set; }
    public string? clientName { get; set; }
    public string? clientEmail { get; set; }
    public string? addressForDelivery { get; set; }
    public DateTime dateOrdered { get; set; }
    public DateTime dateSent { get; set; }
    public DateTime dateDelivered { get; set; }

    public override string ToString() => $@"
       Order ID: {orderId},
       Client Name: {clientName},
       Client Email: {clientEmail},
       Address For Delivery: {addressForDelivery},
       Date Ordered: {dateOrdered},
       Date Sent: {dateSent},
       Date Delivered: {dateDelivered}";
}
