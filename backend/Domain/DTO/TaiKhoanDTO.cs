using System;

public class TaiKhoanDTO
{
	public int id { get; set; }
    public string tenTaiKhoan { get; set; }
    public int loaiTaiKhoanId { get; set; }
    public double soDu { get; set; }

    public TaiKhoanDTO()
	{
        this.id = -1;
	}
}
