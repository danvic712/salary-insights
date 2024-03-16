import { useState } from "react";

import {
  Button,
  Layout,
  Nav,
  Select,
  Typography,
  Card,
  Row,
  Col,
} from "@douyinfe/semi-ui";
import { IconSemiLogo } from "@douyinfe/semi-icons";
import { IconFaq, IconLocaleProvider } from "@douyinfe/semi-icons-lab";

export default function Login() {
  let year = new Date().getFullYear();

  const { Header, Content, Footer } = Layout;
  const { Text } = Typography;

  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const handleLogin = async (e) => {
    e.preventDefault();
    // Here you would usually send a request to your backend to authenticate the user
    // For the sake of this example, we're using a mock authentication
    if (username === "user" && password === "password") {
      // Replace with actual authentication logic
      await login({ username });
    } else {
      alert("Invalid username or password");
    }
  };

  return (
    <div
      style={{
        display: "flex",
        flexDirection: "column",
        minHeight: "100vh",
        background: "rgb(var(--semi-grey-0))",
      }}
    >
      <Header>
        <Nav
          mode="horizontal"
          header={{
            logo: <IconSemiLogo style={{ height: "36px", fontSize: 36 }} />,
            text: "Salary Insights",
            link: "/",
          }}
          footer={
            <>
              <Button
                theme="borderless"
                icon={<IconFaq size="large" />}
                style={{
                  color: "var(--semi-color-text-2)",
                  marginRight: "12px",
                }}
              />
              <Select
                defaultValue="Chinese"
                style={{
                  width: 120,
                  color: "var(--semi-color-text-2)",
                  marginRight: "12px",
                }}
                insetLabel={<IconLocaleProvider />}
              >
                <Select.Option value="Chinese">中文</Select.Option>
                <Select.Option value="English">English</Select.Option>
              </Select>
            </>
          }
        ></Nav>
      </Header>
      <Content
        style={{
          flex: 1,
        }}
      >
        <Row type="flex" justify="space-around" align="middle">
          <Col span={10}>
            <div>
              <form onSubmit={handleLogin}>
                <div>
                  <label htmlFor="username">Username:</label>
                  <input
                    id="username"
                    type="text"
                    value={username}
                    onChange={(e) => setUsername(e.target.value)}
                  />
                </div>
                <div>
                  <label htmlFor="password">Password:</label>
                  <input
                    id="password"
                    type="password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                  />
                </div>
                <button type="submit">Login</button>
              </form>
            </div>
          </Col>
        </Row>
      </Content>
      <Footer
        style={{
          display: "flex",
          color: "var(--semi-color-text-2)",
          fontSize: "14px",
          padding: "20px",
          justifyContent: "center",
          flexShrink: 0,
        }}
      >
        <span>
          Copyright © {year}{" "}
          <Text
            link={{
              href: "https://github.com/danvic712",
              target: "_blank",
            }}
          >
            Danvic Wang
          </Text>{" "}
          All Rights Reserved.{" "}
        </span>
      </Footer>
    </div>
  );
}
