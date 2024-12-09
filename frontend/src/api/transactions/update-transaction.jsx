import { useMutation, useQueryClient } from "@tanstack/react-query";
import { api } from "~/configs/api";

// API để cập nhật giao dịch
export const updateTransaction = async ({id, data}) => {
    const formattedData = {
        ...data,
        taiKhoanChuyenId: data.taiKhoanChuyen,
        taiKhoanNhanId: data.taiKhoanNhan,
        theLoaiId: data.theLoai,
    }
    return api.put(`/transactions/${id}`, formattedData);
};

export const useUpdateTransaction = () => {
    return useMutation({
        mutationFn: ({id, data}) => updateTransaction({id, data}),
        onSuccess: (data) => {
            console.log("Successfully updated transaction. ", data);
        },
        onError: (error) => {
            console.log("Unable to update transaction. ", error);
        },
    });
}