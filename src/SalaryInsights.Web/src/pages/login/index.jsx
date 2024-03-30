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
  Image,
  Form,
  Notification,
} from "@douyinfe/semi-ui";
import { IconSemiLogo, IconUser, IconLock } from "@douyinfe/semi-icons";
import { IconFaq, IconLocaleProvider } from "@douyinfe/semi-icons-lab";

export default function Login() {
  let year = new Date().getFullYear();

  const { Header, Content, Footer } = Layout;
  const { Text, Title } = Typography;

  const handleLogin = async (values) => {
    console.log(values);
    Notification.success({
      title: "Login success",
      content: "Welcome back, Danvic Wang!",
      onClose: () => {
        window.location.href = "/";
      },
    });
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
        <Row
          type="flex"
          justify="space-around"
          align="middle"
          style={{ height: "70vh" }}
        >
          <Col span={8}>
            <Card
              shadows="hover"
              style={{
                textAlign: "center",
              }}
            >
              <Image
                height={72}
                width={72}
                preview={false}
                src="https://lf9-static.semi.design/obj/semi-tos/template/caee33dd-322d-4e91-a4ed-eea1b94605bb.png"
              />
              <Title heading={2} style={{ margin: "15px 0px" }}>
                Salary Insights
              </Title>
              <Form onSubmit={(values) => handleLogin(values)}>
                {({ formState, values, formApi }) => (
                  <>
                    <Form.Input
                      label={{ text: "用户名" }}
                      prefix={<IconUser />}
                      field="userName"
                      placeholder="输入用户名"
                      block={true}
                      rules={[
                        { required: true, message: "required error" },
                        { type: "string", message: "type error" },
                        {
                          validator: (rule, value) => value === "semi",
                          message: "should be semi",
                        },
                      ]}
                    />
                    <Form.Input
                      label={{ text: "密码" }}
                      prefix={<IconLock />}
                      field="password"
                      mode="password"
                      placeholder="输入密码"
                      block={true}
                      rules={[
                        { required: true, message: "required error" },
                        { type: "string", message: "type error" },
                        {
                          validator: (rule, value) => value === "semi",
                          message: "should be semi",
                        },
                      ]}
                    />

                    <Button
                      block={true}
                      htmlType="submit"
                      theme="solid"
                      style={{ margin: "12px 0px" }}
                    >
                      登录
                    </Button>
                  </>
                )}
              </Form>
            </Card>
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
