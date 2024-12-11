import { Line } from "react-chartjs-2"; // Import Line chart từ react-chartjs-2
import { Chart as ChartJS, Tooltip, Legend, LineElement, CategoryScale, LinearScale, PointElement } from "chart.js";

ChartJS.register(CategoryScale, LinearScale, PointElement, LineElement, Tooltip, Legend);

export const LineChartMonthly = ({ transactions }) => {
    const chartData = {
        labels: transactions.map(item => item.month), // Các tháng
        datasets: [
          {
            label: "Thu nhập",
            data: transactions.map(item => item.income), // Thu nhập theo tháng
            borderColor: "#32C77E",
            backgroundColor: "rgba(50, 199, 126, 0.2)",
            fill: true, // Đổ màu phía dưới đường
            tension: 0.4, // Điều chỉnh độ cong của đường
          },
          {
            label: "Chi tiêu",
            data: transactions.map(item => item.expense), // Chi tiêu theo tháng
            borderColor: "#FF4D4F",
            backgroundColor: "rgba(255, 77, 79, 0.2)",
            fill: true,
            tension: 0.4,
          },
        ],
      };
    
      // Cấu hình cho biểu đồ (các tùy chọn khác)
    const chartOptions = {
        responsive: true,
        plugins: {
          title: {
            display: true,
            text: "Thu chi theo tháng năm 2024",
          },
          tooltip: {
            mode: "index",
            intersect: false,
          },
        },
        scales: {
          x: {
            title: {
              display: true,
              text: "Tháng",
            },
          },
          y: {
            title: {
              display: true,
              text: "Tổng tiền (VND)",
            },
            beginAtZero: true, // Đảm bảo trục y bắt đầu từ 0
          },
        },
      };

    return <Line data={chartData} options={chartOptions} />;
}