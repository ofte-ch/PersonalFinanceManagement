import { create } from "zustand";

export const useTransactionStore = create((set) => ({
  transaction: null,
  openCreateModal: false,
  openUpdateModal: false,
  openDeleteModal: false,
  setTransaction: (transaction) => set({ transaction }),
  setOpenCreateModal: (openCreateModal) => set({ openCreateModal }),
  setOpenUpdateModal: (openUpdateModal) => set({ openUpdateModal }),
  setOpenDeleteModal: (openDeleteModal) => set({ openDeleteModal }),
}));
