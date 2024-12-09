import {
  Button,
  Form,
  Input,
  Modal,
  Row,
  Col,
  message,
  Select,
  DatePicker,
  InputNumber,
} from "antd";
import { EditOutlined, EditFilled } from "@ant-design/icons";
import { Flex } from "antd";
import { useUpdateTransaction } from "~/api/transactions/update-transaction";
import { getAllAccounts } from "~/api/accounts/get-accounts";
import { useTransactionStore } from "~/stores/transactions/transactionStore";
import { useEffect, useState } from "react";
import { useTypes } from "~/api/types/get-types";
import moment from "moment";

const { TextArea } = Input;
const UpdateTransactionModal = () => {
  const [form] = Form.useForm();
  const { data: types } = useTypes();
  const { openUpdateModal, setOpenUpdateModal, setTransaction, transaction } =
    useTransactionStore();
  const [accounts, setAccounts] = useState([]);
  useEffect(() => {
    getAllAccounts().then((accounts) => setAccounts(accounts));
  }, []);

  const mutation = useUpdateTransaction({
    onSuccess: () => {
      form.resetFields();
      message.success("Transaction updated successfully");
    },
    onFinish: () => {
      form.resetFields();
      message.error("Failed to update transaction !");
    },
  });

  const onFinish = (values) => {
    var fomattedNgayGiaoDich = values.ngayGiaoDich.format("YYYY-MM-DDTHH:mm:ss");
    values.ngayGiaoDich = fomattedNgayGiaoDich;
    mutation.mutate({
      id: transaction.id,
      data: values,
    });
    setTransaction(null);
    setOpenUpdateModal(false);
  };

  const handleCloseUpdateModal =() =>{
    setTransaction(null);
    form.resetFields();
    setOpenUpdateModal(false);
  }

  useEffect(() => {
    if (transaction) {
      form.setFieldsValue({
        ...transaction,
        ngayGiaoDich: moment(transaction.ngayGiaoDich, "YYYY-MM-DD HH:mm:ss"),
        taiKhoanChuyen: transaction.taiKhoanChuyen ? transaction.taiKhoanChuyen.id : null,
        taiKhoanNhan: transaction.taiKhoanNhan ? transaction.taiKhoanNhan.id : null,
        theLoai: transaction.theLoai ? transaction.theLoai.id : [],
      });
    }
  }, [transaction, form]);

  return (
    <Modal
      title={
        <div>
            <h2 style={{margin: 0}}>Cập nhật giao dịch</h2>
            <Button
              icon={<EditFilled/>}
              style={{ marginTop: "10px", height:"fit-content"}}>
                Edit
            </Button>
        </div>
      }
      open={openUpdateModal}
      onCancel={handleCloseUpdateModal}
      footer={null}
      maskClosable={false} 
    >
      <Form
        form={form}
        onFinish={onFinish}
        className="pt-4"
        layout="vertical"
        variant="filled"
      >
        <Row gutter={16}>
          <Col span={12}>
            <Form.Item
              label="Tên giao dịch"
              name="tenGiaoDich"
              rules={[{ required: true, message: "Nhập tên giao dịch" }]}
            >
              <Input placeholder="Nhập tên giao dịch..." />
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item
              label="Ngày giao dịch"
              name="ngayGiaoDich"
              rules={[{ required: true, message: "Chọn ngày giao dịch" }]}
            >
              <DatePicker style={{ width: "100%" }} />
            </Form.Item>
          </Col>
        </Row>
        <Row gutter={16}>
          <Col span={12}>
            <Form.Item
              label="Tài khoản chuyển"
              name="taiKhoanChuyen"
              dependencies={["taiKhoanChuyen"]}
              rules={[{ required: true, message: "Chọn tài khoản chuyển !" }]}
            >
              <Select placeholder="Chọn tài khoản chuyển...">
                {accounts?.map((account) => (
                  <Option key={account.id} value={account.id}>
                    {account.tenTaiKhoan} - {account.soDu} VND
                  </Option>
                ))}
              </Select>
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item
              label="Tài khoản nhận"
              name="taiKhoanNhan"
              dependencies={["taiKhoanChuyen"]}
              rules={[
                ({ getFieldValue }) => ({
                  validator(_, value) {
                    if (!value || getFieldValue("taiKhoanChuyen") !== value) {
                      return Promise.resolve();
                    }
                    return Promise.reject(
                      new Error(
                        "Tài khoản nhận không được trùng tài khoản chuyển !"
                      )
                    );
                  },
                }),
              ]}
            >
              <Select placeholder="Chọn tài khoản nhận....">
                <Option
                  key="0"
                  value={null}
                  placeholder="Chọn tài khoản nhận...."
                ></Option>
                {accounts?.map((account) => (
                  <Option key={account.id} value={account.id}>
                    {account.tenTaiKhoan} - {account.soDu} VND
                  </Option>
                ))}
              </Select>
            </Form.Item>
          </Col>
        </Row>
        <Row gutter={16}>
          <Col span={12}>
            <Form.Item
              label="Thể loại"
              name="theLoai"
              rules={[{ required: true, message: "Chọn thể loại !" }]}
            >
              <Select placeholder="Chọn thể loại....">
                {types?.map((type) => (
                  <Option key={type.id} value={type.id}>
                    {type.tenTheLoai}
                  </Option>
                ))}
              </Select>
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item
              label="Số tiền giao dịch"
              name="tongTien"
              rules={[
                {
                  required: true,
                  message: "Vui lòng nhập chi phí giao dịch !",
                },
                {
                  validator: (_, value) => {
                    if (!value) {
                      return Promise.resolve();
                    }
                    const numericValue = Number(value);
                    if (value && !isNaN(numericValue) && numericValue > 0) {
                      return Promise.resolve();
                    }
                    return Promise.reject(
                      new Error("Số tiền giao dịch phải là số hợp lệ!")
                    );
                  },
                },
              ]}
            >
              <Input placeholder="Nhập chi phí giao dịch...." />
            </Form.Item>
          </Col>
        </Row>
        <Form.Item
          label="Ghi chú"
          name="ghiChu"
        >
          <TextArea row={4} placeholder="Ghi chú giao dịch...." />
        </Form.Item>
        <Form.Item className="pt-4 m-0">
          <Flex justify="end" className="gap-3">
            <Button loading={false} type="default" htmlType="reset">
              Reset
            </Button>
            <Button loading={false} type="primary" htmlType="submit">
              Submit
            </Button>
          </Flex>
        </Form.Item>
      </Form>
    </Modal>
  );
};
export default UpdateTransactionModal;
