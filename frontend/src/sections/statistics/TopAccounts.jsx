import React from "react";

export const TopAccounts = ({ title, data, dataKey }) => {
    return (
        <div className="p-4 bg-white rounded-lg shadow-md">
            <h2 className="text-xl font-semibold mb-4">{title}</h2>
            {data.length === 0 ? (
                <p className="text-gray-500">Không có dữ liệu</p>
            ) : (
                <table className="min-w-full divide-y divide-gray-200">
                    <thead className="bg-gray-50">
                        <tr>
                            <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Tài khoản</th>
                            <th className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Tổng tiền</th>
                        </tr>
                    </thead>
                    <tbody className="bg-white divide-y divide-gray-200">
                        {data.map((item) => (
                            <tr key={item.taiKhoanId}>
                                <td className="px-6 py-4 whitespace-nowrap">{item.tenTaiKhoan}</td>
                                <td className="px-6 py-4 whitespace-nowrap">{item[dataKey]}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            )}
        </div>
    )
}