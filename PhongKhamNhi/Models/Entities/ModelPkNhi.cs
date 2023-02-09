using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace PhongKhamNhi.Models.Entities
{
    public partial class ModelPkNhi : DbContext
    {
        public ModelPkNhi()
            : base("name=ModelPkNhi")
        {
        }

        public virtual DbSet<BacSi> BacSis { get; set; }
        public virtual DbSet<BenhNhi> BenhNhis { get; set; }
        public virtual DbSet<ChiNhanh> ChiNhanhs { get; set; }
        public virtual DbSet<DichVuKham> DichVuKhams { get; set; }
        public virtual DbSet<DoanhThu> DoanhThus { get; set; }
        public virtual DbSet<HoaDonBanThuoc> HoaDonBanThuocs { get; set; }
        public virtual DbSet<KetQuaXN> KetQuaXNs { get; set; }
        public virtual DbSet<LoaiThuoc> LoaiThuocs { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<PhieuDangKyKham> PhieuDangKyKhams { get; set; }
        public virtual DbSet<PhieuDKXN> PhieuDKXNs { get; set; }
        public virtual DbSet<PhieuKham_Thuoc> PhieuKham_Thuoc { get; set; }
        public virtual DbSet<PhieuKhamBenh> PhieuKhamBenhs { get; set; }
        public virtual DbSet<Quyen> Quyens { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<Thuoc> Thuocs { get; set; }
        public virtual DbSet<Thuoc_HoaDon> Thuoc_HoaDon { get; set; }
        public virtual DbSet<ThuocBan> ThuocBans { get; set; }
        public virtual DbSet<ToNhanVien> ToNhanViens { get; set; }
        public virtual DbSet<XetNghiem> XetNghiems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BacSi>()
                .Property(e => e.Sdt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BenhNhi>()
                .Property(e => e.SdtThanNhan)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ChiNhanh>()
                .HasMany(e => e.DoanhThus)
                .WithRequired(e => e.ChiNhanh)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ChiNhanh>()
                .HasMany(e => e.ThuocBans)
                .WithRequired(e => e.ChiNhanh)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DichVuKham>()
                .HasMany(e => e.XetNghiems)
                .WithMany(e => e.DichVuKhams)
                .Map(m => m.ToTable("DichVuKham_XN").MapLeftKey("MaDV").MapRightKey("MaXN"));

            modelBuilder.Entity<HoaDonBanThuoc>()
                .Property(e => e.Sdt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<HoaDonBanThuoc>()
                .HasMany(e => e.Thuoc_HoaDon)
                .WithRequired(e => e.HoaDonBanThuoc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.Sdt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.HoaDonBanThuocs)
                .WithOptional(e => e.NhanVien)
                .HasForeignKey(e => e.MaNvLap);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.HoaDonBanThuocs1)
                .WithOptional(e => e.NhanVien1)
                .HasForeignKey(e => e.MaNvThu);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.KetQuaXNs)
                .WithRequired(e => e.NhanVien)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.PhieuKhamBenhs)
                .WithOptional(e => e.NhanVien)
                .HasForeignKey(e => e.MaNvLap);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.PhieuKhamBenhs1)
                .WithOptional(e => e.NhanVien1)
                .HasForeignKey(e => e.MaNvThu);

            modelBuilder.Entity<PhieuDangKyKham>()
                .Property(e => e.Sdt)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PhieuDKXN>()
                .HasMany(e => e.KetQuaXNs)
                .WithRequired(e => e.PhieuDKXN)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PhieuKhamBenh>()
                .HasMany(e => e.PhieuKham_Thuoc)
                .WithRequired(e => e.PhieuKhamBenh)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.TenDangNhap)
                .IsUnicode(false);

            modelBuilder.Entity<TaiKhoan>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<Thuoc>()
                .HasMany(e => e.PhieuKham_Thuoc)
                .WithRequired(e => e.Thuoc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Thuoc>()
                .HasMany(e => e.Thuoc_HoaDon)
                .WithRequired(e => e.Thuoc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Thuoc>()
                .HasMany(e => e.ThuocBans)
                .WithRequired(e => e.Thuoc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<XetNghiem>()
                .HasMany(e => e.KetQuaXNs)
                .WithRequired(e => e.XetNghiem)
                .WillCascadeOnDelete(false);
        }
    }
}
