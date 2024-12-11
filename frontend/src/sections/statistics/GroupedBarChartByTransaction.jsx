import React from "react";
import { Bar } from "react-chartjs-2";
import { Chart as ChartJS, CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend } from "chart.js";


ChartJS.register(CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend);

export const GroupedBarChartByTransaction = ({ transactionData }) => {
    const labels = transactionData.map((item) => item.tenTheLoai);
    const data = {
        labels,
        datasets: [
            {
                label: "Thu",
                data: transactionData.map((item) => item.tongThu),
                backgroundColor: "rgb(54, 162, 235)",
            },
            {
                label: "Chi",
                data: transactionData.map((item) => item.tongChi),
                backgroundColor: "rgb(255, 99, 132)",
            },
        ]
    }

    const options = {
        responsive: true,
        plugins: {
            legend: {
                position: "top",
            },
            title: {
                display: true,
                text: "Thu & Chi theo loại giao dịch",
            },
        }
    }

    return <Bar data={data} options={options} />
}


