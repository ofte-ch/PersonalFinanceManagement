import React from "react";
import { Bar } from "react-chartjs-2";
import { Chart as ChartJS, CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend } from "chart.js";


ChartJS.register(CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend);

export const GroupedBarChartByAccount = ({ transactionData }) => {
    const labels = transactionData.map((item) => item.tenTaiKhoan);
    const data = {
        labels,
        datasets: [
            {
                label: "Receipts",
                data: transactionData.map((item) => item.tongThu),
                backgroundColor: "rgb(54, 162, 235)",
            },
            {
                label: "Expenditures",
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
                text: "Receipts vs Expenditures by Account",
            },
        }
    }

    return <Bar data={data} options={options} />
}


