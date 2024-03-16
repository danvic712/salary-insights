import {
  Col,
  Row,
  Card,
  Popover,
  Descriptions,
  Progress,
  Tabs,
  TabPane,
  List,
  Typography,
} from "@douyinfe/semi-ui";
import {
  IconArrowDown,
  IconArrowUp,
  IconInfoCircle,
} from "@douyinfe/semi-icons";

import "./index.scss";

export default function Index() {
  const { Text, Title } = Typography;
  const { Item } = Descriptions;

  return (
    <div className="anlyanis-container" style={{ margin: "16px" }}>
      <Row gutter={[16, 16]}>
        <Col span={6}>
          <Card shadows="hover">
            <Descriptions
              data={[{ key: "总收入", value: "¥ 100000000" }]}
              size="large"
              row={true}
            />
          </Card>
        </Col>
        <Col span={6}>
          <Card shadows="hover">
            <Descriptions
              data={[{ key: "今年收入·", value: "¥ 1000000" }]}
              size="large"
              row={true}
            />
          </Card>
        </Col>
        <Col span={6}>
          <Card shadows="hover">
            <Descriptions
              data={[{ key: "今年收入·", value: "¥ 1000000" }]}
              size="large"
              row={true}
            />
          </Card>
        </Col>
        <Col span={6}>
          <Card
            style={{ height: 230 }}
            footerLine={true}
            shadows="hover"
            footer={
              <span>
                日访问量<span style={{ paddingLeft: 10 }}>5,396</span>
              </span>
            }
          >
            <div className="flex-between">
              <span>访问量</span>{" "}
              <Popover
                position="top"
                showArrow
                content={<article>指标说明</article>}
              >
                <IconInfoCircle
                  style={{ color: "var(--semi-color-primary)" }}
                />
              </Popover>
            </div>
            <Descriptions row size="large">
              <Item itemKey="">9,384</Item>
            </Descriptions>
          </Card>
        </Col>
      </Row>
      <Row gutter={[16, 16]}>
        <Col span={16}>
          <Card title="Card Title" bordered={false} shadows="hover">
            Card Content
          </Card>
        </Col>
        <Col span={8}>
          <Card title="Card Title" bordered={false} shadows="hover">
            Card Content
          </Card>
        </Col>
      </Row>
    </div>
  );
}
