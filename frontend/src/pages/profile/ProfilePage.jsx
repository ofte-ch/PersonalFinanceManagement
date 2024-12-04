import { useAuthStore } from "~/stores/auth/authStore";
import { Card, Typography, Form, Input, Button, Space, message } from "antd";
import { useState, useEffect } from "react";
import { useUpdateUser } from "~/api/auth/update-user";
import { EyeOutlined, EyeInvisibleOutlined } from "@ant-design/icons";
const ProfilePage = () => {
  const { user } = useAuthStore();
  console.log(user);
  const [form] = Form.useForm();
  const [isEditing, setIsEditing] = useState(false);
  const [oldPasswordVisible, setOldPasswordVisible] = useState(false);
  const [newPasswordVisible, setNewPasswordVisible] = useState(false);
  const [repeatNewPasswordVisible, setRepeatNewPasswordVisible] =
    useState(false);

  useEffect(() => {
    if (user) {
      form.setFieldsValue({
        name: user.name,
        username: user.username,
      });
    }
  }, [user, form]);

  const updateMutation = useUpdateUser({
    onSuccess: () => {
      message.success("Cập nhật chỉnh sửa thành công !");
      setIsEditing(false);
    },
    onError: (error) => {
      console.log(error);
      message.error("Password is incorrect ! Try again later.");
      setIsEditing(false);
    },
  });

  const onFinish = (values) => {
    const formattedValues = {
      userId: user.id,
      name: values.name,
      oldPassword: values.oldPassword,
      newPassword: values.newPassword,
    };
    updateMutation.mutate(formattedValues);
  };

  const handleCancel = () => {
    form.resetFields();
    setIsEditing(false);
  };
  return (
    <>
      <div className="flex items-center justify-center min-h-screen bg-gray-200 rounded ">
        <Card
          className="w-[450px] shadow-lg rounded"
          title={
            <Typography.Title level={4} className="text-center">
              Thông tin cá nhân
            </Typography.Title>
          }
          bordered={false}
        >
          <Form
            form={form}
            layout="vertical"
            onFinish={onFinish}
            initialValues={{
              name: user.name,
              username: user.username,
            }}
          >
            <Form.Item
              label={
                <Typography.Text className="block mb-2 font-medium text-gray-700">
                  Họ Tên :
                </Typography.Text>
              }
              name="name"
              rules={[
                { required: true, message: "Họ tên không được để trống !" },
                {
                  pattern:
                    /^[a-zA-Zàáảãạâấầẩẫậăắằẳẵặèéẻẽẹêếềểễệìíỉĩịòóỏõọôốồổỗộơớờởỡợùúủũụưứừửữựỳýỷỹỵđ\s]+$/,
                  message: "Họ tên chỉ được chứa chữ cái và khoảng trắng!",
                },
                {
                  validator(_, value) {
                    if (!value || value.trim().length >= 1) {
                      return Promise.resolve();
                    }
                    return Promise.reject(
                      new Error("Họ tên phải chứa ít nhất 1 ký tự!")
                    );
                  },
                },
              ]}
            >
              <Input disabled={!isEditing} />
            </Form.Item>
            <Form.Item
              label={
                <Typography.Text className="block mb-2 font-medium text-gray-700">
                  Tên đăng nhập :
                </Typography.Text>
              }
              name="username"
            >
              <Input disabled={true} />
            </Form.Item>
            {isEditing && (
              <>
                <Form.Item
                  label={
                    <Typography.Text className="block mb-2 font-medium text-gray-700">
                      Mật khẩu cũ :
                    </Typography.Text>
                  }
                  name="oldPassword"
                  rules={[
                    {
                      required: true,
                      message: "Mật khẩu không được để trống !",
                    },
                    {
                      min: 6,
                      message: "Mật khẩu phải có ít nhất 6 ký tự",
                    },
                  ]}
                >
                  <Input
                    className="max-h-10"
                    type={oldPasswordVisible ? "text" : "password"}
                    disabled={!isEditing}
                    suffix={
                      isEditing && (
                        <Button
                          className="h-full"
                          type="text"
                          icon={
                            oldPasswordVisible ? (
                              <EyeInvisibleOutlined />
                            ) : (
                              <EyeOutlined />
                            )
                          }
                          onClick={() =>
                            setOldPasswordVisible(!oldPasswordVisible)
                          }
                        />
                      )
                    }
                  />
                </Form.Item>
                <Form.Item
                  label={
                    <Typography.Text className="block mb-2 font-medium text-gray-700">
                      Mật khẩu mới :
                    </Typography.Text>
                  }
                  dependencies={["oldPassword"]}
                  name="newPassword"
                  rules={[
                    {
                      required: true,
                      message: "Mật khẩu mới không được để trống !",
                    },
                    {
                      min: 6,
                      message: "Mật khẩu mới phải có ít nhất 6 ký tự",
                    },
                    ({ getFieldValue }) => ({
                      validator(_, value) {
                        if (!value || getFieldValue("oldPassword") !== value) {
                          return Promise.resolve();
                        }
                        return Promise.reject(
                          new Error(
                            "Mật khẩu mới không được trùng mật khẩu cũ !"
                          )
                        );
                      },
                    }),
                  ]}
                >
                  <Input
                    className="max-h-10"
                    type={newPasswordVisible ? "text" : "password"}
                    disabled={!isEditing}
                    suffix={
                      isEditing && (
                        <Button
                          className="h-full"
                          type="text"
                          icon={
                            newPasswordVisible ? (
                              <EyeInvisibleOutlined />
                            ) : (
                              <EyeOutlined />
                            )
                          }
                          onClick={() =>
                            setNewPasswordVisible(!newPasswordVisible)
                          }
                        />
                      )
                    }
                  />
                </Form.Item>
                <Form.Item
                  label={
                    <Typography.Text className="block mb-2 font-medium text-gray-700">
                      Nhập lại mật khẩu mới :
                    </Typography.Text>
                  }
                  name="repeatNewPassword"
                  dependencies={["newPassword"]}
                  rules={[
                    {
                      required: true,
                      message: "Nhập lại mật khẩu mới ! ",
                    },
                    ({ getFieldValue }) => ({
                      validator(_, value) {
                        if (!value || getFieldValue("newPassword") === value) {
                          return Promise.resolve();
                        }
                        return Promise.reject(
                          new Error("Nhập lại mật khẩu không khớp!")
                        );
                      },
                    }),
                  ]}
                >
                  <Input
                    className="max-h-10"
                    type={repeatNewPasswordVisible ? "text" : "password"}
                    disabled={!isEditing}
                    suffix={
                      isEditing && (
                        <Button
                          className="h-full"
                          type="text"
                          icon={
                            repeatNewPasswordVisible ? (
                              <EyeInvisibleOutlined />
                            ) : (
                              <EyeOutlined />
                            )
                          }
                          onClick={() =>
                            setRepeatNewPasswordVisible(
                              !repeatNewPasswordVisible
                            )
                          }
                        />
                      )
                    }
                  />
                </Form.Item>
              </>
            )}
            <div className="flex justify-center space-x-4">
              {isEditing ? (
                <>
                  <Button onClick={handleCancel}>Hủy</Button>
                  <Button type="primary" htmlType="submit">
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
        </Card>
      </div>
    </>
  );
};
export default ProfilePage;
