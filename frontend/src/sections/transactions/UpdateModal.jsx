import { Form, Input, Select, DatePicker, InputNumber, 
        Button, Typography, Space } from "antd";
import { Modal } from "antd/lib";
import { EditOutlined, EditFilled } from "@ant-design/icons";
import moment from "moment";
import React, { useEffect, useState } from "react";
import { useMutation } from "@tanstack/react-query";
import { useUpdateTransaction } from "~/api/transactions/update-transaction";

const { TextArea } = Input;
const { Option } = Select;
 
const UpdateTransactionModal = ({transaction, setSelectedTransaction, isOpened, setOpenUpdateModal}) => {
    // Form instance
    const [form] = Form.useForm();
    // Title
    const [title, setTitle] = useState("");
    // Check if you are editing
    const [isEditing, setIsEditing] = useState(false);
    // Set transaction whenever data is loaded
    useEffect( () => {
        if(transaction){
            form.setFieldsValue({
                ...transaction,
                ngayGiaoDich:moment(transaction.ngayGiaoDich)
            });
            setTitle(`Transaction ${transaction.id}`);
        }
    }, [transaction, form]);
    // Toggle edit mode
    const handleEditToggle = () => {
        setIsEditing((prev) => !prev); 
    };
    // Mutation update
    const {mutate: updateTransactionMutation, isLodaing: isUpdating } = useUpdateTransaction();
    // Handle update
    const onFinish = (values) => {
        console.log('Form values:', values);
        updateTransactionMutation(transaction?.id, ...values);
    };
    const onFinishFailed = (errorInfo) => {
        console.log('Failed:', errorInfo);
    };

    const handleCloseModal = () => {
        setOpenUpdateModal(false);
        setSelectedTransaction(null);
    }

    return (
        <Space direction="vertical" sixze="medium" >
            <Modal 
                className="bg-panel text-elements-primary"
                content={{style:{className:"bg-elements"}}}
                open={isOpened} 
                title={title}
                maskClosable={false} 
                onCancel={() => handleCloseModal()}
                okButtonProps={{style: { display:"none" } }}
                width="50%"
                centered
            >
                {/* Edit button */}
                <Button 
                    icon={<EditOutlined/>}
                    onClick={() => handleEditToggle()} 
                >
                    <strong>
                        {isEditing ? "Cancel Edit": "Edit"}
                    </strong>
                </Button>

                {/* Form */}        
                <Form
                    form={form}
                    title={`Transaction #`}
                    layout="horizontal"
                    labelCol={{span: 7,}}
                    wrapperCol={{span: 30,}}
                    onFinish={onFinish}
                    onFinishFailed={onFinishFailed}
                    labelAlign="left"
                    autoComplete="off"
                >
                    {/* ID */}
                    <Form.Item
                        label="ID"
                        name="id"
                        tooltip="This field is locked and cannot be edited."                    
                        rules={[{ required: true, message: 'Please input the ID!' }]}
                    >
                        <Input width="20px" disabled/>
                    </Form.Item>
                    {/* Name */}
                    <Form.Item
                        label="Name"
                        name="tenGiaoDich"
                        rules={[{ required: true, message: 'Please input the name!' }]}
                    >
                        <Input placeholder="Enter name" disabled={!isEditing}/>
                    </Form.Item>

                    {/* Date */}
                    <Form.Item
                        label="Date"
                        name="ngayGiaoDich"
                        rules={[{ required: true, message: 'Please select a date!' }]}
                    >
                        <DatePicker style={{ width: '100%' }} disabled={!isEditing} />
                    </Form.Item>

                    {/* Type */}
                    <Form.Item
                        label="Type"
                        name="loaiGiaoDich"
                        rules={[{ required: true, message: 'Please select a type!' }]}
                    >
                        <Select placeholder="Select transaction type" disabled={!isEditing}>
                            <Option value="chi">Chi</Option>
                            <Option value="thu" >Thu</Option>
                        </Select>
                    </Form.Item>

                    {/* Total (Money) */}
                    <Form.Item
                        label="Total"
                        name="tongTien"
                        rules={[{ required: true, message: 'Please input the total!' }]}
                    >
                        <InputNumber
                        style={{ width: '100%' }}
                        placeholder="Enter total amount"
                        disabled={!isEditing}
                        formatter={(value) => `VND ${value}`.replace(/\B(?=(\d{3})+(?!\d))/g, ',')}
                        parser={(value) => value.replace(/\$\s?|(,*)/g, '')}
                        />
                    </Form.Item>

                    {/* Note */}
                    <Form.Item label="Note" name="ghiChu">
                        <TextArea rows={4} disabled={!isEditing} placeholder="Detail information !!"/>
                    </Form.Item>

                    {/* Submit Button */}
                    <Form.Item>
                    {isEditing && (
                        <Button htmlType="submit" style={{ marginLeft: 8 }}>
                            Save
                        </Button>
                    )}
                    </Form.Item>
                </Form>
            </Modal>
        </Space>
    )
}

export default UpdateTransactionModal;