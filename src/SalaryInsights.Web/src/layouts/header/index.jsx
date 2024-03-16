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
            items: ["每月薪资", "数据工具"],
          },
          {
            itemKey: "setting",
            text: "系统配置",
            icon: <IconConfig size="large" />,
          },
        ]}
        renderWrapper={({ itemElement, props }) => {
          const routerMap = {
            dashboard: "/analysis",
            每月薪资: "/payroll",
            setting: "/settings",
            数据工具: "/data-tools",
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
