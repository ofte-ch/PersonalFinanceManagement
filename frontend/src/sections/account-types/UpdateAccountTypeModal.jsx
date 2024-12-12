import { Button, Form, Input, Modal, Row, Col, message, Spin } from "antd";
import { Flex } from "antd";
import { useUpdateAccountType } from "~/api/account-types/update-account-type";
import { useAccountTypeStore } from "~/stores/account-types/accountTypeStore";
import { useEffect, useState } from "react";

const UpdateAccountTypeModal = () => {
  const [form] = Form.useForm();
  const { openUpdateModal, setOpenUpdateModal, setAccountType, accountType } = useAccountTypeStore();
  const [isEditing, setIsEditing] = useState(false);
  const [isDirty, setIsDirty] = useState(false);

  const handleCloseUpdateModal = () => {
    setAccountType(null);
    setIsEditing(false);
    form.resetFields();
    setOpenUpdateModal(false);
  }

  const handleCancelWhileEditing = () => {
    if (isDirty) {
      Modal.confirm({
        title: "Bạn có chắc muốn hủy chỉnh sửa ?",
        content: "Những thay đổi chưa được lưu sẽ bị mất.",
        okText: "Hủy chỉnh sửa",
        cancelText: "Tiếp tục chỉnh sửa",
        onOk: () => {
          form.resetFields();
          setIsEditing(false);
          setIsDirty(false);
        },
      });
    } else {
      form.resetFields();
      setIsEditing(false);
    }
  };

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
      maskClosable={false}
      footer={null}
    >
      <Form
        form={form}
        onFinish={onFinish}
        className="pt-4"
        layout="vertical"
        variant="filled"
        initialValues={{
          ten: accountType?.ten,
        }}
        onValuesChange={() => setIsDirty(true)}
      >
        <Row gutter={16}>
          <Col span={24}>
            <Form.Item
              label="Tên loại tài khoản"
              name="ten"
              rules={[{ required: true, message: "Nhập tên loại tài khoản" }]}
            >
              <Input placeholder="Nhập tên loại tài khoản..." disabled={!isEditing}/>
            </Form.Item>
          </Col>
        </Row>
        <div className="flex justify-center space-x-5">
        {isEditing ? (
          <>
            <Button onClick={handleCancelWhileEditing}>Hủy</Button>
            <Button
              type="primary"
              htmlType="submit"
            >
              {mutation.isPending && (
                <Spin size="small" style={{ marginRight: 8 }} />
              )}
              Lưu
            </Button>
          </>
        ) : (
          <Button type="default" onClick={() => setIsEditing(true)}>
            Chỉnh sửa
          </Button>
        )}
        </div>
      </Form>
    </Modal>
  );
};
export default UpdateAccountTypeModal;
