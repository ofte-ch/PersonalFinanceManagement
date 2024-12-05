import {
  Button,
  Form,
  Input,
  Modal,
  Select,
  Row,
  Col,
  message,
  DatePicker,
} from "antd";
import { Flex } from "antd";
import { useTransactionStore } from "~/stores/transactions/transactionStore";
import { useCreateTransaction } from "~/api/transactions/create-transaction";
import { useTypes } from "~/api/types/get-types";
import { getAllAccounts } from "~/api/accounts/get-accounts";
import { useState, useEffect } from "react";
import moment from "moment";

const { TextArea } = Input;
const CreateTransactionModal = () => {
  const [form] = Form.useForm();
  const { openCreateModal, setOpenCreateModal } = useTransactionStore();
  const [accounts, setAccounts] = useState([]);
  useEffect(() => {
    getAllAccounts().then((accounts) => setAccounts(accounts));
  }, []);

  const { data: types } = useTypes();

  const mutation = useCreateTransaction({
    onSuccess: () => {
      form.resetFields();
      setOpenCreateModal(false);
      message.success("Transaction created successfully");
    },
    onFinish: () => {
      message.error("Failed to create new transaction !");
    },
  });

  const onFinish = (values) => {
    const formattedValues = {
      tenGiaoDich: values.tenGiaoDich,
      ngayGiaoDich: moment().format("YYYY-MM-DD HH:mm:ss"),
      taiKhoanChuyen: values.taiKhoanChuyen,
      taiKhoanNhan: values.taiKhoanNhan,
      theLoai: values.theLoai,
      tongTien: values.tongTien,
      ghiChu: values.ghiChu,
    };
    mutation.mutate(formattedValues);
  };
  return (
    <Modal
      title={"Tạo giao dịch "}
      open={openCreateModal}
      onCancel={() => setOpenCreateModal(false)}
      footer={null}
    >
      <Form
        form={form}
        onFinish={onFinish}
        className="pt-4"
        layout="vertical"
        variant="filled"
        initialValues={{
          ngayGiaoDich: moment(),
        }}
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
              <DatePicker
                showTime
                disabled
                format="YYYY-MM-DD"
                style={{ width: "100%" }}
              />
            </Form.Item>
          </Col>
        </Row>
        <Row gutter={16}>
          <Col span={12}>
            <Form.Item
              label="Tài khoản chuyển"
              name="taiKhoanChuyen"
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
                { required: true, message: "Chọn tài khoản nhận !" },
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
                { required: true, message: "Nhập chi phí giao dịch" },
                {},
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
export default CreateTransactionModal;
