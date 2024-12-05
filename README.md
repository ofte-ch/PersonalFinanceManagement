![image](https://github.com/user-attachments/assets/fea0f00b-49ce-4936-bb0a-9184b1c11ef8)![image](https://github.com/user-attachments/assets/1f9e1960-9d58-4771-9a9f-b839578e60af)# Chương trình quản lý tài chính cá nhân

Phần mềm quản lý tài chính cá nhân là công cụ hỗ trợ người dùng theo dõi, lập kế hoạch và quản lý tài chính hàng ngày một cách hiệu quả. Với giao diện thân thiện, các tính năng như ghi chép thu chi, lập ngân sách, theo dõi nợ vay và báo cáo tài chính, phần mềm này giúp người dùng kiểm soát tài chính cá nhân dễ dàng và đưa ra các quyết định tài chính thông minh hơn.

## Các chức năng chính
- Đăng nhập
- Quản lý tài khoản thanh toán
- Quản lý thu chi
- Thống kê chi tiêu và thu nhập
- Cùng các chức năng khác

## Công nghệ sử dụng

![.NET](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![React](https://img.shields.io/badge/React-20232A?style=for-the-badge&logo=react&logoColor=61DAFB)
![MySQL](https://img.shields.io/badge/MySQL-00000F?style=for-the-badge&logo=mysql&logoColor=white)
![JWT](https://img.shields.io/badge/json%20web%20tokens-323330?style=for-the-badge&logo=json-web-tokens&logoColor=pink)

## Cài đặt và sử dụng chương trình

Thực hiện các bước sau (yêu cầu đã cài sẵn Visual Studio, Laragon để đảm bảo trải nghiệm tốt nhất)

1. Clone hoặc tải dự án này về máy cá nhân.
2. Mở dự án trong Visual Studio ở thư mục Backend.
3. Khởi chạy máy chủ MySQL bằng laragon hay bất kỳ phần mềm nào có hỗ trợ hệ quản trị CSDL MySQL, tạo bảng với tên "ef".
4. Điều chỉnh lại thông tin kết nối đến CSDL thông qua chuỗi connection string.
```json
"ConnectionStrings": {
  ...
  "DefaultConnection": "server=localhost;user=root;password=;database=ef"
  ...
},
```
5. Chạy dự án bằng nút Run của IDE để khởi chạy phần xử lý Backend.
6. Mở thư mục frontend, sau đó mở CMD và gõ lệnh sau để khởi chạy phần xử lý Frontend.
```bash
npm i
npm run dev
```
7. Truy cập theo địa chỉ mà màn hình CMD chỉ dẫn để bắt đầu sử dụng ứng dụng.

## Tổng quan ứng dụng thông qua giao diện màn hình

Giao diện trang chủ
![Trang chủ](https://i.imgur.com/zvEZLtc.png)
Giao diện đăng nhập
![Đăng nhập](https://i.imgur.com/a/NBkCsAy.png)
Giao diện quản lý tài khoản giao dịch
![Quản lý tài khoản giao dịch](https://i.imgur.com/NBkCsAy.png)
Giao diện quản lý giao dịch
![Quản lý giao dịch](https://i.imgur.com/HhPscm7.png)
Giao diện thống kê theo loại giao dịch
![Thống kê theo loại giao dịch](https://i.imgur.com/25N1k26.png)
Giao diện thống kê theo tài khoản giao dịch
![Thống kê theo tài khoản giao dịch](https://i.imgur.com/S1uxgAL.png)

## Liên hệ

Bằng cách tạo issue mới thông qua repo này (thanh menu phía trên) nếu cần hỗ trợ hoặc giải đáp các thắc mắc liên quan đến dự án. Chân thành cám ơn mọi người đã quan tâm!
