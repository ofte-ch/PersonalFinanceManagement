import { useMutation, useQueryClient } from "@tanstack/react-query";
import { api } from "~/axios/api";

export const getStatisticByAccounts = async ({ tuNgay, denNgay }) => {
    try {
        const response = await api.post(`/thongke/byaccount`, {
            tuNgay,
            denNgay,
        });
        console.log(response.data);
        return response.data;
    }
    catch (error) {
        console.error("API Error: ", error);
        throw new Error(error);
    }
   
}

export const useStatisticByAccounts = (options = {}) => {
    const { onSuccess, onError, ...restConfig } = options;

    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: getStatisticByAccounts,
        onSuccess: (data, ...args) => {
          queryClient.invalidateQueries({
            queryKey: ["accounts"],
          });
          onSuccess?.(data, ...args);
          console.log(data);
        },
        onError: (error, ...args) => {
          onError?.(error, ...args);
        },
        ...restConfig,
    });
}
