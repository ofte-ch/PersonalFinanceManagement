import { DatePicker, Space, Button, message, Select } from "antd";
import { useState, useRef } from "react";
import PageHeader from "~/components/page-header";
import { ByTransactionTable } from "~/sections/statistics/ByTransactionTable";
import { useStatisticByTransactions } from "~/api/statistics/create-statistic-by-transactions";
import moment from "moment";
import { GroupedBarChart } from "~/sections/statistics/GroupedBarChart";
import { getTopCategories } from "~/api/statistics/get-top-categories";
import { TopCategories} from "~/sections/statistics/TopCategories";

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
    const [topIncome, setTopIncome] = useState([]);
    const [topExpense, setTopExpense] = useState([]);

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
                if (date.isSame(today, "day")) {
                    setDenNgay(today.clone());
                }

                else if (date.isAfter(today, "day")) {
                    message.warning("Cannot select a future date");
                    setTuNgay(null);
                    setDenNgay(null);
                }

                else {
                    setDenNgay(date.clone().endOf("day"));

                }
                break;
            case "week":
            {
                const startOfWeek = date.clone().startOf("week");
                const endOfWeek = date.clone().endOf("week");


                if (endOfWeek.isSame(today, "week")) {
                    setTuNgay(startOfWeek);
                    setDenNgay(today.clone());
                }

                else if (endOfWeek.isAfter(today, "day")) {
                    message.warning("Cannot select a future week");
                    setDenNgay(null);
                    setTuNgay(null);
                }
                else {
                    setTuNgay(startOfWeek);
                    setDenNgay(endOfWeek);
                }

                break;
            }
            case "month":
            {
                const startOfMonth = date.clone().startOf("month");
                const endOfMonth = date.clone().endOf("month");

                if (date.isSame(today, "month")) {
                    setTuNgay(startOfMonth);
                    setDenNgay(today.clone());
                }
                else if (endOfMonth.isAfter(today, "m")) {
                    message.warning("Cannot select a future month");
                    setDenNgay(null);
                    setTuNgay(null);

                }
                else {
                    setTuNgay(startOfMonth);
                    setDenNgay(endOfMonth);
                }
                
                break;
            }
            case "year":
            {
                const startOfYear = date.clone().startOf("year");
                const endOfYear = date.clone().endOf("year");

                if (date.isSame(today, "year")) {
                    setTuNgay(startOfYear);
                    setDenNgay(today.clone());

                }
                else if (endOfYear.isAfter(today, "day")) {
                    message.warning("Cannot select a future year");
                    setDenNgay(null);
                    setTuNgay(null);
                }
                else {
                    setTuNgay(startOfYear);
                    setDenNgay(endOfYear);
                }
                
                
                break;
            }
            default:
                setDenNgay(null);
        }
    };

    // luu lai trang thai truoc do
    const lastDates = useRef(null);

    // xu ly chon ngay theo khoang thoi gian
    const handleCustomRangeChange = (dates) => {
        if (!dates) {
            setTuNgay(null);
            setDenNgay(null);
            return;
        }
        
        if (
            lastDates.current &&
            lastDates.current[0]?.isSame(dates[0], "day") &&
            lastDates.current[1]?.isSame(dates[1], "day")
        ) {
            // Nếu giống, không làm gì cả
            return;
        }
    
        // Lưu lại giá trị mới
        lastDates.current = dates;
    
        const [startDate, endDate] = dates;
    
        if (endDate.isAfter(today, "day")) {
            if (!tuNgay || !denNgay) {
                console.log("1");
                message.warning("Cannot select a future date");
            }
            setTuNgay(null);
            setDenNgay(null);
            return;
        }
    
        setTuNgay(startDate.clone().startOf("day"));
    
        if (endDate.isSame(today, "day")) {
            // Nếu ngày cuối cùng là ngày hiện tại
            setDenNgay(today.clone());
        } else {
            // Ngày cuối cùng là ngày trong quá khứ
            setDenNgay(endDate.clone().endOf("day"));
        }
    };
    

    // xu ly khi nhan submit
    const handleSubmit = () => {
        if (!tuNgay || !denNgay) {
            message.warning("Please select the date");
            return;
        }

       mutate({ tuNgay, denNgay }, {
        onSuccess: (transactionData) => {
            const topIncomeCategories = getTopCategories(transactionData, "tongThu");
            setTopIncome(topIncomeCategories);

            const topExpenseCategories = getTopCategories(transactionData, "tongChi");
            setTopExpense(topExpenseCategories);
       },
       onError: (error) => {
              message.error(error.message);
        }
    }
    );

    
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
                
        <main className="flex-grow overflow-y-auto">
            <div className={`w-full max-w-3xl mx-auto h-80 ${transactionData && transactionData.length > 0 ? '' : 'hidden' }`}>
            
                <GroupedBarChart transactionData={transactionData} />
            

            </div>
        </main>
        
        <ByTransactionTable data={transactionData} />
        </Space>

        <div className="grid grid-cols-2 gap-4 mt-4">
            <TopCategories title="Top 5 Income Categories" data={topIncome} dataKey="tongThu" />
            <TopCategories title="Top 5 Expense Categories" data={topExpense} dataKey="tongChi" />
        </div>
        
        </>
    );
};

export default StatisticByTransactionPage;
