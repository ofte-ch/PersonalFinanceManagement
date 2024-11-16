import { DatePicker, Space, Button, message, Select } from "antd";
import { useState } from "react";
import PageHeader from "~/components/page-header";
import { ByTransactionTable } from "~/sections/statistics/ByTransactionTable";
import { useStatisticByTransactions } from "~/api/statistics/create-statistic-by-transactions";
import moment from "moment";

const { RangePicker } = DatePicker;
const {MonthPicker, YearPicker} = DatePicker;

// tao option lua chon loai thong ke
const options = [
  { value: "day", label: "By Day" },
  { value: "week", label: "By Week" },
  { value: "month", label: "By Month" },
  { value: "year", label: "By Year" },
  { value: "custom", label: "By Period" },
];

// Cấu hình để tuần bắt đầu từ Thứ Hai
moment.updateLocale("vi", {
    week: {
      dow: 1, // Thứ Hai là ngày đầu tiên trong tuần
      doy: 4, // Ngày đầu tiên của năm
    },
});

const StatisticByTransactionPage = () => {
    const [tuNgay, setTuNgay] = useState(null);
    const [denNgay, setDenNgay] = useState(null);
    const [selectedOption, setSelectedOption] = useState("day");

    const [transactionData, setTransactionData] = useState([]);

    const { mutate, isLoading } = useStatisticByTransactions({
        onSuccess: (data) => {
            setTransactionData(data);
        },
        onError: (error) => {
            message.error(error.message);
        },
    });

    // xu ly su kien loc thong ke
    const handleSelectedOption = (value) => {
        setSelectedOption(value);
        setTuNgay(null);
        setDenNgay(null);
    };

    // tu dong cong ngay vao denNgay dua theo selectedOption
    const handleTuNgayChange = (date) => {
        if (!date) {
            setTuNgay(null);
            setDenNgay(null);
            return;
        }

        setTuNgay(date);

        // tu dong cong ngay vao denNgay dua theo selectedOption
        switch (selectedOption) {
            case "day":
                setDenNgay(date.clone().endOf("day"));
                break;
            case "week":
                setDenNgay(date.clone().endOf("week"));
                break;
            case "month":
                setTuNgay(date.clone().startOf("month"));
                setDenNgay(date.clone().endOf("month"));
                break;
            case "year":
                setTuNgay(date.clone().startOf("year"));
                setDenNgay(date.clone().endOf("year"));
                break;
            default:
                setDenNgay(null);
        }
    };

    // xu ly chon ngay theo khoang thoi gian
    const handleCustomRangeChange = (dates) => {
        if (!dates) {
          setTuNgay(null);
          setDenNgay(null);
          return;
        }
        setTuNgay(dates[0]);
        setDenNgay(dates[1]);
    };

    // xu ly khi nhan submit
    const handleSubmit = () => {
        if (!tuNgay || !denNgay) {
            message.warning("Please select the date");
            return;
        }

       mutate({ tuNgay, denNgay });
    };


    return (
        <>
        <Space direction="vertical" size="middle" style={{ width: "100%" }}>
            <PageHeader
                heading="Transaction Statistics"
                links={[
                    { title: "Dashboard", href: "/dashboard" },
                    { title: "Statistics By Transactions" },
                ]}
            />
            <Space>
                <Select
                    options={options}
                    value={selectedOption}
                    onChange={handleSelectedOption}
                    style={{ width: 200 }}
                    placeholder="Chọn loại thống kê"
                />
                {/* Theo ngày */}
                {selectedOption === "day" && (
                    <DatePicker
                        onChange={handleTuNgayChange}
                        value={tuNgay}
                    />
                )}

                {/* Theo tuần */}
                {selectedOption === "week" && (
                <Space>
                    <DatePicker
                        onChange={handleTuNgayChange}
                        value={tuNgay}
                    />
                    <DatePicker
                        value={denNgay}
                        disabled // Ngày cuối tuần tự động tính
                    />
                </Space>
                )}

                {/* Theo tháng */}
                {selectedOption === "month" && (
                <MonthPicker
                    picker="month"
                    onChange={handleTuNgayChange}
                    value={tuNgay}
                />
                )}

                {/* Theo năm */}
                {selectedOption === "year" && (
                <YearPicker
                    picker="year"
                    onChange={handleTuNgayChange}
                    value={tuNgay}
                />
                )}

                {/* Khoảng thời gian */}
                {selectedOption === "custom" && (
                <RangePicker
                    onChange={handleCustomRangeChange}
                    value={tuNgay && denNgay ? [tuNgay, denNgay] : null}
                />
                )}
            </Space>

            <Button
                type="primary"
                onClick={handleSubmit}
                loading={isLoading}
                className="mb-3"
            >Filter</Button>
            
        </Space>

        <ByTransactionTable data={transactionData} />
        </>
    );
};

export default StatisticByTransactionPage;
