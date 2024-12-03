
import { Bar } from "react-chartjs-2";
import { Chart as ChartJS, CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend } from "chart.js";
import PropTypes from 'prop-types';


ChartJS.register(CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend);

export const GroupedBarChartByTransaction = ({ transactionData }) => {

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

    const labels = safeTransactionData.map((item) => item.tenTheLoai);
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
                text: "Receipts vs Expenditures by Category",
            },
        }
    }

    return <Bar data={data} options={options} />
}

GroupedBarChartByTransaction.propTypes = {
    transactionData: PropTypes.array.isRequired,
};


