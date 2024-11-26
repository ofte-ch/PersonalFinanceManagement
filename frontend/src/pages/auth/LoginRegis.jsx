import React, { useState } from "react";
import "./LoginRegis.css";
import { useLogin } from "~/api/auth/login";
import { useRegister } from "~/api/auth/register";
import { Link, useLocation, useNavigate } from "react-router-dom";
function LoginToggle() {
  const [isSignUp, setIsSignUp] = useState(false);
  const [error,setError] = useState(-1);
  const [regisPass, setRegisPass] = useState("");
  const [regisCPass, setRegisCPass] = useState("");
  const location = useLocation();
  const navigate = useNavigate();

  const loginMutation = useLogin({
    onSuccess: () => {
      message.success("Login successful");
      const form = document.getElementById("loginForm");
      if (form) {
        form.reset();
      }
      navigate("/dashboard");
    },
    onError: (error) => {
      console.log("Error:", error);
      message.error("Email or password is incorrect");
    },
  });
  
  const registerMutation = useRegister({
    onSuccess: () => {
      message.success("Register successful");
      const form = document.getElementById("regisForm");
      if (form) {
        form.reset();
      }
      navigate("/auth/login");
    },
    onError: (error) => {
      console.log("Error:", error);
      message.error("Email or password is incorrect");
    },
  });


  const login = (event) => {
    const formData = event.target;
    const values = {
      username: formData.username.value,
      password: formData.password.value,
    };
    loginMutation.mutate(values);
  };
  const register = (event) => {
    const formData = event.target;
    const values = {
      name: formData.name.value,
      username: formData.username.value,
      password: formData.password.value,

    };
    registerMutation.mutate(values);
  }

  const validatePassword = () => {
    if (regisPass.length < 6 ) {
      setError(1);
      return false;
    }
    setError(-1); 
    return true;
  };

  const validateConfirmPassword = () => {
    if (regisPass !== regisCPass) {
      setError(2);
      return false;
    }
    setError(-1); 
    return true;
  };

  return (
    <div
      className={`login-wrapper relative min-w-[768px] max-w-full min-h-[480px] bg-white rounded-3xl shadow-xl overflow-hidden ${
        isSignUp ? "active" : ""
      }`}
    >
      {/* Sign-Up Form */}
      <div
        className={`form-wrapper absolute top-0 h-full w-1/2 transition-all duration-300 ${
          isSignUp ? "translate-x-full opacity-100 z-10" : "opacity-0 z-0"
        }`}
      >
        <form
          id="regisForm"
          onSubmit={(e) => {
            e.preventDefault();
            register(e);
          }}
          className="flex flex-col items-center px-10 h-full"
        >
          <h1 className="text-2xl font-bold mb-4 pt-5">Tạo Tài Khoản</h1>
          <input
            type="text"
            placeholder="Tên"
            name="name"
            className="input-field"
            required
          />
          <input
            type="email"
            placeholder="Email"
            name="username"
            className="input-field"
            required
          />
          <input
            type="password"
            placeholder="Mật khẩu"
            name="password"
            className="input-field"
            value={regisPass}
            onChange={(e) => setRegisPass(e.target.value)}
            onBlur={validatePassword}
            required
          />
          {error === 1 && <p className="text-red-500 text-sm mt-2">* Password must have at least 6 characters !</p>}
          <input
            type="password"
            placeholder="Nhập lại mật khẩu"
            className="input-field"
            value={regisCPass}
            onChange={(e) => setRegisCPass(e.target.value)}
            onBlur={validateConfirmPassword}
            required
          />
          {error === 2 && <p className="text-red-500 text-sm mt-2">* Repeat password is different !</p>}
          <button
            type="submit"
            className=" bg-slate-500 hover:bg-slate-600 text-white uppercase text-sm py-2 px-6 rounded-md cursor-pointer mt-4 hover:bg-gradient-to-l"
          >
            Đăng Kí
          </button>
        </form>
      </div>

      {/* Sign-In Form */}
      <div
        className={`form-wrapper absolute top-0 h-full w-1/2 transition-all duration-300 ${
          isSignUp ? "-translate-x-full opacity-0 z-0" : "opacity-100 z-10"
        }`}
      >
        <form
          id="loginForm"
          onSubmit={(e) => {
            e.preventDefault();
            login(e);
          }}
          className="flex flex-col items-center px-10 h-full"
        >
          <h1 className="text-2xl font-bold mb-4 pt-5">Đăng Nhập</h1>
          <input
            type="text"
            placeholder="Email"
            name="username"
            className="input-field"
            required
          />
          <input
            type="password"
            placeholder="Mật khẩu"
            name="password"
            className="input-field"
            required
          />
          <a href="/forgot-password" className="text-sm text-gray-500 mt-2">
            Quên mật khẩu?
          </a>
          <button
            type="submit"
            className=" bg-slate-500 hover:bg-slate-600 text-white uppercase text-sm py-2 px-6 rounded-md cursor-pointer mt-4 hover:bg-gradient-to-l"
          >
            Đăng Nhập
          </button>
        </form>
      </div>

      {/* Toggle Container */}
      <div
        className={`toggle-container absolute top-0 right-0 w-1/2 h-full transition-all duration-300 ${
          isSignUp ? "translate-x-[-100%]" : "translate-x-0"
        }`}
      >
        <div className="toggle bg-gradient-to-r from-slate-600 to-blue-200 h-full flex flex-col items-center justify-center text-center">
          {isSignUp ? (
            <>
              <h1 className="text-3xl font-bold text-white">
                Chào mừng trở lại!
              </h1>
              <p className="text-white text-sm mt-2">Đăng nhập để tiếp tục</p>
              <button
                className="bg-slate-500 hover:bg-slate-600 uppercase text-sm py-2 px-6 rounded-md cursor-pointer mt-4 text-white hover:bg-transparent-medium"
                onClick={() => setIsSignUp(false)}
              >
                Đăng nhập
              </button>
            </>
          ) : (
            <>
              <h1 className="text-3xl font-bold text-white">Xin chào!</h1>
              <p className="text-white text-sm mt-2">
                Đăng kí để trải nghiệm tính năng
              </p>
              <button
                className="bg-slate-500 hover:bg-slate-600 uppercase text-sm py-2 px-6 rounded-md cursor-pointer mt-4 text-white "
                onClick={() => setIsSignUp(true)}
              >
                Đăng kí
              </button>
            </>
          )}
        </div>
      </div>
    </div>
  );
}

export default LoginToggle;
