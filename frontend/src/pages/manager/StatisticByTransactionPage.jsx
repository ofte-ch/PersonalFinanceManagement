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

    // lay ngay hien tai
    const today = moment();

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
                if (date.isAfter(today, "day")) {
                    message.warning("Cannot select a future date");
                    setTuNgay(null);
                    return;
                }
                setDenNgay(date.clone().endOf("day"));
                break;
            case "week":
            {
                const endOfWeek = date.clone().endOf("week");

                if (endOfWeek.isAfter(today, "day")) {
                    setDenNgay(today.clone().endOf("day"));
                }
                else {
                    setDenNgay(endOfWeek);
                }

                break;
            }
            case "month":
            {
                const endOfMonth = date.clone().endOf("month");
                if (date.isSame(today, "month")) {
                    setDenNgay(today.subtract(1, "day").endOf("day"));
                }
                else if (endOfMonth.isAfter(today, "day")) {
                    setDenNgay(today.clone().endOf("day"));

                }
                else {
                    setDenNgay(endOfMonth);
                }
                setTuNgay(date.clone().startOf("month")); // Ngày đầu tháng
                break;
            }
            case "year":
            {
                const endOfYear = date.clone().endOf("year");
                if (date.isSame(today, "year")) {
                    setDenNgay(today.subtract(1, "day").endOf("day"));

                }
                else if (endOfYear.isAfter(today, "day")) {
                    message.warning("Cannot select a future year");
                    setDenNgay(null);
                    setTuNgay(null);
                }
                else {
                    setDenNgay(endOfYear);
                }
                
                setTuNgay(date.clone().startOf("year")); // Ngày đầu năm
                break;
            }
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
        
        const [startDate, endDate] = dates;


        if (endDate.isAfter(today, "day")) {
            message.warning("Cannot select a future date");
            return;
            
        }

        setTuNgay(startDate.clone().startOf("day"));
        setDenNgay(endDate.clone().endOf("day"));
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
