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
  Select,
  Typography,
} from "@douyinfe/semi-ui";

import { VChart } from "@visactor/react-vchart";

import {
  IconArrowDown,
  IconArrowUp,
  IconInfoCircle,
} from "@douyinfe/semi-icons";

import "./index.scss";

export default function Index() {
  const barChart = {
    spec: {
      type: "bar",
      width: 400,
      height: 200,
      data: [
        {
          id: "barData",
          values: [
            {
              name: "Apple",
              value: 214480,
            },
            {
              name: "Google",
              value: 155506,
            },
            {
              name: "Amazon",
              value: 100764,
            },
            {
              name: "Microsoft",
              value: 92715,
            },
            {
              name: "Coca-Cola",
              value: 66341,
            },
            {
              name: "Samsung",
              value: 59890,
            },
            {
              name: "Toyota",
              value: 53404,
            },
            {
              name: "Mercedes-Benz",
              value: 48601,
            },
            {
              name: "Facebook",
              value: 45168,
            },
          ],
        },
      ],
      direction: "horizontal",
      xField: "value",
      yField: "name",
      axes: [
        {
          orient: "bottom",
          visible: false,
        },
      ],
      label: {
        visible: true,
      },
    },
  };

  const lineChart = {
    spec: {
      type: "line",
      data: {
        values: [
          { type: "1月", country: " 2023", value: 4229 },
          { type: "1月", country: "2024", value: 4376 },
          { type: "2月", country: " 2023", value: 5323 },
          { type: "2月", country: "2024", value: 6200 },
          { type: "3月", country: " 2023", value: 5400 },
          { type: "3月", country: "2024", value: 5500 },
          { type: "4月", country: " 2023", value: 4229 },
          { type: "4月", country: "2024", value: 4376 },
          { type: "5月", country: " 2023", value: 4229 },
          { type: "5月", country: "2024", value: 4376 },
          { type: "6月", country: " 2023", value: 14229 },
          { type: "6月", country: "2024", value: 4376 },
          { type: "7月", country: " 2023", value: 4229 },
          { type: "7月", country: "2024", value: 0 },
          { type: "8月", country: " 2023", value: 4229 },
          { type: "8月", country: "2024", value: 0 },
          { type: "9月", country: " 2023", value: 4229 },
          { type: "9月", country: "2024", value: 0 },
          { type: "10月", country: " 2023", value: 4229 },
          { type: "10月", country: "2024", value: 0 },
          { type: "11月", country: " 2023", value: 4229 },
          { type: "11月", country: "2024", value: 0 },
          { type: "12月", country: " 2023", value: 4229 },
          { type: "12月", country: "2024", value: 0 },
        ],
      },
      stack: true,
      xField: "type",
      yField: "value",
      seriesField: "country",
      lineLabel: {
        visible: true,
        syncState: true,
        state: {
          blur: { opacity: 0.2 },
        },
      },
      legends: [{ visible: true, position: "middle", orient: "bottom" }],
      line: {
        state: {
          highlight: {
            lineWidth: 4,
          },
          blur: {
            opacity: 0.2,
          },
        },
      },
      point: {
        state: {
          blur: {
            opacity: 0.2,
          },
        },
      },
      hover: false,
      interactions: [
        {
          type: "element-highlight-by-group",
        },
      ],
    },
  };

  const { Text, Title } = Typography;
  const { Item } = Descriptions;

  return (
    <div className="anlyanis-container" style={{ margin: "16px" }}>
      <Row gutter={[16, 16]}>
        <Col span={8}>
          <Card shadows="hover">
            <Descriptions
              data={[{ key: "总收入", value: "¥ 100000000" }]}
              size="large"
              row={true}
            />
          </Card>
        </Col>

        <Col span={8}>
          <Card
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
        <Col span={8}>
          <Card
            title="月度最高收入排行"
            shadows="hover"
            headerExtraContent={
              <Select defaultValue="5" insetLabel="收入最高">
                <Select.Option value="5">5 月</Select.Option>
                <Select.Option value="10">10 月</Select.Option>
              </Select>
            }
          >
            <VChart
              spec={{
                ...barChart.spec,
              }}
            />
          </Card>
        </Col>
      </Row>
      <Row gutter={[16, 16]}>
        <Col span={16}>
          <Card title="年度收入" bordered={false} shadows="hover">
            <VChart
              spec={{
                ...lineChart.spec,
              }}
            />
          </Card>
        </Col>
      </Row>
    </div>
  );
}
