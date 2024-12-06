using System;

public class TheLoaiDTO
{
    public int id { get; set; }
    public string tenTheLoai { get; set; }

    public string moTa { get; set; }

    public string phanLoai { get; set; }

    public TheLoaiDTO()
    {
    }
    public TheLoaiDTO(int id, string tenTheLoai, string moTa, string phanLoai)
    {
        this.id = id;
        this.tenTheLoai = tenTheLoai;
        this.moTa = moTa;
        this.phanLoai = phanLoai;
    }

}
