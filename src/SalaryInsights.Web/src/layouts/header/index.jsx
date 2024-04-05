import { Link } from "react-router-dom";
import {
  Layout,
  Nav,
  Button,
  Avatar,
  Dropdown,
  Select,
} from "@douyinfe/semi-ui";
import { IconSemiLogo } from "@douyinfe/semi-icons";
import {
  IconBanner,
  IconConfig,
  IconDescriptions,
  IconFaq,
  IconLocaleProvider,
} from "@douyinfe/semi-icons-lab";

export default function HeaderComponent() {
  const { Header } = Layout;

  return (
    <Header>
      <Nav
        mode="horizontal"
        header={{
          logo: <IconSemiLogo style={{ height: "36px", fontSize: 36 }} />,
          text: "Salary Insights",
          link: "/",
        }}
        items={[
          {
            itemKey: "dashboard",
            text: "数据看板",
            icon: <IconBanner size="large" />,
          },
          {
            itemKey: "dataCenter",
            text: "数据中心",
            icon: <IconDescriptions size="large" />,
            items: ["每月薪资", "薪资分析"],
          },
          {
            itemKey: "setting",
            text: "系统管理",
            icon: <IconConfig size="large" />,
            items: ["基础配置", "用户中心"],
          },
        ]}
        renderWrapper={({ itemElement, props }) => {
          const routerMap = {
            dashboard: "/dashboard",
            每月薪资: "/payroll",
            基础配置: "/settings",
            薪资分析: "/data-tools",
            用户中心: "/users",
          };
          return (
            <Link
              style={{ textDecoration: "none" }}
              to={routerMap[props.itemKey]}
            >
              {itemElement}
            </Link>
          );
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
              defaultValue="zh_CN"
              style={{
                width: 140,
                color: "var(--semi-color-text-2)",
                marginRight: "12px",
              }}
              insetLabel={<IconLocaleProvider />}
            >
              <Select.Option value="zh_CN">简体中文</Select.Option>
              <Select.Option value="en_US">English</Select.Option>
            </Select>
            <Dropdown
              position="bottomRight"
              render={
                <Dropdown.Menu>
                  <Dropdown.Item>详情</Dropdown.Item>
                  <Dropdown.Item>退出</Dropdown.Item>
                </Dropdown.Menu>
              }
            >
              <Avatar size="small" color="light-blue" style={{ margin: 4 }}>
                DW
              </Avatar>
            </Dropdown>
          </>
        }
        defaultSelectedKeys={["dashboard"]}
      ></Nav>
    </Header>
  );
}
