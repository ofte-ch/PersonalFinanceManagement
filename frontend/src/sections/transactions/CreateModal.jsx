import { Form, Input, Select, DatePicker, InputNumber, Button } from "antd";
import { Modal } from "antd/lib";
import moment from "moment";
import React, { useEffect, useMemo, useState } from "react";

const { TextArea } = Input;
const { Option } = Select;

const AddNewTransactionModal = ({accountList, currentMaxId, setCurrentMaxId, isOpened, setOpenAddingModal}) => {
    const [form] = Form.useForm();
    console.log(accountList);
    
    const onFinish = (values) => {
        console.log('Form values:', values);
    };

    const onFinishFailed = (errorInfo) => {
        console.log('Failed:', errorInfo);
    };
    
    return (
        <>
        <Modal 
            className="modal-create-transaction bg-panel text-elements-primary"
            content={{style:{className:"bg-elements"}}}
            title="Add new transaction"
            open={isOpened} 
            maskClosable={false} 
            onCancel={() => setOpenAddingModal(false)}
            okButtonProps={{style: { htmlType:'submit'} }} 
            centered
            width="50%"
        >
            <Form
                form={form}s
                layout="vertical"
                onFinish={onFinish}
                onFinishFailed={onFinishFailed}
                labelAlign="left"
                autoComplete="off"
                initialValues={{
                    ngayGiaoDich: moment().startOf('day'), // Hoặc dùng dayjs().startOf('day') nếu dùng Day.js
                }}>
                <div style={{ display: "flex", gap: "20px" }}>
                    <Form.Item
                        label="ID"
                        name="id"
                        style={{ flex: 1 }}
                    >
                        <Input style={{ width: '100%' }} disabled/>
                    </Form.Item>
                    <Form.Item
                        label="Ngày tạo giao dịch"
                        name="ngayGiaoDich"
                        style={{ flex: 1 }}
                    >
                        <DatePicker style={{ width: '100%' }} disabled/>
                    </Form.Item>
                </div>
                <Form.Item
                    label="Tên giao dịch"
                    name="tenGiaoDich"
                    rules={[{ required: true, message: 'Vui lòng nhập tên giao dịch !!!'}]}
                >
                    <Input placeholder="Vui lòng nhập tên giao dịch ..." />
                </Form.Item>
                {/* Tài khoản gửi và tài khoản nhận */}
                <div style={{ display: "flex", gap: "20px" }}>
                    <Form.Item
                        label="Tài khoản chuyển"
                        name="taiKhoanChuyen"
                        rules={[{ required: true, message: 'Vui lòng chọn tài khoản chuyển !!!' }]}
                        style={{ flex: 1 }}
                    >
                        <Select>
                            {accountList.map((account) => (
                            <Option key={account.id} value={account.id}>
                                {account.tenTaiKhoan}
                            </Option>
                            ))}
                        </Select>
                    </Form.Item>
                    <Form.Item
                        label="Tài khoản nhậnt"
                        name="taiKhoanNhan"
                        rules={[{ required: true, message: 'Vui lòng chọn tài khoản nhận !!!' }]}
                        style={{ flex: 1 }}
                    >
                        <Select>
                            {accountList.map((account) => (
                            <Option key={account.id} value={account.id}>
                                {account.tenTaiKhoan}
                            </Option>
                            ))}
                        </Select>
                    </Form.Item>
                </div>
                {/* Tổng tiền và số dư */}
                <div style={{ display: "flex", gap: "20px" }}>
                    <Form.Item
                        label="Tổng tiền"
                        name="tongTien"
                        rules={[{ required: true, message: 'Vui lòng nhập tổng tiền !!!' }]}
                        style={{ flex: 1 }}
                    >
                        <InputNumber
                            style={{ width: '100%' }}
                            formatter={(value) => `$ ${value}`.replace(/\B(?=(\d{3})+(?!\d))/g, ',')}
                            parser={(value) => value.replace(/\$\s?|(,*)/g, '')}
                        />
                    </Form.Item>
                    <Form.Item
                        label="Số dư"
                        name="soDu"
                        style={{ flex: 1 }}
                    >
                        <InputNumber
                            style={{ width: '100%' }}
                            prePlaceholder="0"
                            formatter={(value) => `$ ${value}`.replace(/\B(?=(\d{3})+(?!\d))/g, ',')}
                            parser={(value) => value.replace(/\$\s?|(,*)/g, '')}
                            disabled
                        />
                    </Form.Item>
                </div>

                {/* Ghi chú riêng 1 hàng */}
                <Form.Item label="Ghi chú" name="ghiChu">
                    <TextArea rows={4} placeholder="Ghi chú giao dịch ..." />
                </Form.Item>
            </Form>
        </Modal>
        </>
    )
}

export default AddNewTransactionModal;