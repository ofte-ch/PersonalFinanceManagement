import {
  Button,
  Form,
  Input,
  Modal,
  Select,
  Row,
  Col,
  message,
} from "antd";
import { Flex } from "antd";
import { useCreateAccount } from "~/api/accounts/create-account";
import { useAccountStore } from "~/stores/accounts/accountStore";
import { useAccountTypes } from "~/api/account-types/get-account-types";
const CreateAccountModal = () => {
  const [form] = Form.useForm();
  const {data: accountTypes} = useAccountTypes();
  const { openCreateModal, setOpenCreateModal } = useAccountStore();

  const mutation = useCreateAccount({
    onSuccess: () => {
      form.resetFields();
      setOpenCreateModal(false);
      message.success("Account created successfully");
    },
    onFinish: () => {
      message.error("Failed to create new account !");
    },
  });

  const onFinish = (values) => {
    mutation.mutate(values);
  };
  return (
    <Modal
      title={"Tạo tài khoản"}
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
      >
        <Row gutter={16}>
          <Col span={12}>
            <Form.Item
              label="Tên tài khoản"
              name="tenTaiKhoan"
              rules={[{ required: true, message: "Nhập tên tài khoản" }]}
            >
              <Input placeholder="Nhập tên tài khoản..." />
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item
              label="Loại tài khoản"
              name="loaiTaiKhoanId"
              rules={[{ required: true, message: "Chọn loại tài khoản" }]}
            >
              <Select placeholder="Chọn loại tài khoản">
                {accountTypes?.map((accountType) => (<Option key = {accountType.id} value = {accountType.id}>{accountType.ten}</Option>))}
              </Select>
            </Form.Item>
          </Col>
        </Row>
        <Row gutter={16}>
          <Col span={12}>
            <Form.Item
              label="Số dư"
              name="soDu"
              rules={[{ required: true, message: "Nhập số dư" }]}
            >
              <Input placeholder="Nhập số dư..." />
            </Form.Item>
          </Col>
        </Row>
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
export default CreateAccountModal;
