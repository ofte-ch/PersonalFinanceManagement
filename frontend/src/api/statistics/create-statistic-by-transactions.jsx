import { useMutation, useQueryClient } from "@tanstack/react-query";
import { api } from "~/axios/api";

export const getStatisticByTransactions = ({ tuNgay, denNgay }) => {
    return api.post(`/thongke/bytransactiontype`, {
        tuNgay,
        denNgay,
    });
}

export const useStatisticByTransactions = (options = {}) => {
    const { onSuccess, onError, ...restConfig } = options;

    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: getStatisticByTransactions,
        onSuccess: (data, ...args) => {
          queryClient.invalidateQueries({
            queryKey: ["transactions"],
          });
          onSuccess?.(data, ...args);
        },
        onError: (error, ...args) => {
          onError?.(error, ...args);
        },
        ...restConfig,
    });
}
