
import { Bar } from "react-chartjs-2";
import { Chart as ChartJS, CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend } from "chart.js";
import PropTypes from 'prop-types';

ChartJS.register(CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend);

export const GroupedBarChartByAccount = ({ transactionData }) => {

    const safeTransactionData = (() => {
        if (Array.isArray(transactionData)) return transactionData;
        if (typeof transactionData === "object" && transactionData !== null) return Object.values(transactionData);
        if (typeof transactionData === "string") {
            try {
                const parsedData = JSON.parse(transactionData);
                return Array.isArray(parsedData) ? parsedData : [parsedData];
            } catch {
                console.error("Invalid JSON string");
            }
        }
        return [];
    })();
    

    const labels = safeTransactionData.map((item) => item.loaiTaiKhoan);
    const data = {
        labels,
        datasets: [
            {
                label: "Receipts",
                data: safeTransactionData.map((item) => item.tongThu),
                backgroundColor: "rgb(54, 162, 235)",
            },
            {
                label: "Expenditures",
                data: safeTransactionData.map((item) => item.tongChi),
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

    console.log(transactionData);
    console.log(safeTransactionData);

    return <Bar data={data} options={options} />
}

GroupedBarChartByAccount.propTypes = {
    transactionData: PropTypes.array.isRequired,
};

