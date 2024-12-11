import { Button, Form, Input, Modal, Row, Col, message } from "antd";
import { Flex } from "antd";
import { useUpdateAccountType } from "~/api/account-types/update-account-type";
import { useAccountTypeStore } from "~/stores/account-types/accountTypeStore";
import { useEffect } from "react";

const UpdateAccountTypeModal = () => {
  const [form] = Form.useForm();
  const { openUpdateModal, setOpenUpdateModal, setAccountType, accountType } = useAccountTypeStore();

  const handleCloseUpdateModal = () => {
    setAccountType(null);
    form.resetFields();
    setOpenUpdateModal(false);
  }
  const mutation = useUpdateAccountType({
    onSuccess: () => {
      form.resetFields();
      message.success("Cập nhật loại tài khoản thành công !");
    },
    onFinish: () => {
      message.error("Cập nhật loại tài khoản thất bại !!!");
    },
  });

  const onFinish = (values) => {
    mutation.mutate({
      id: accountType.id,
      data: values,
    });
    setOpenUpdateModal(false);
  };

  useEffect(() => {
    if (accountType) {
      form.setFieldsValue({
        ...accountType,
      });
    }
  }, [accountType, form]);

  return (
    <Modal
      title={"Cập nhật loại tài khoản"}
      open={openUpdateModal}
      onCancel={handleCloseUpdateModal}
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
          <Col span={24}>
            <Form.Item
              label="Tên loại tài khoản"
              name="ten"
              rules={[{ required: true, message: "Nhập tên loại tài khoản" }]}
            >
              <Input placeholder="Nhập tên loại tài khoản..." />
            </Form.Item>
          </Col>
          
        </Row>
        
        <Form.Item className="pt-4 m-0">
          <Flex justify="end" className="gap-3">
            <Button loading={false} type="default" htmlType="reset">
              Reset
            </Button>
            <Button loading={false} type="primary" htmlType="submit">
              Lưu
            </Button>
          </Flex>
        </Form.Item>
      </Form>
    </Modal>
  );
};
export default UpdateAccountTypeModal;
