import { SideSheet } from "@douyinfe/semi-ui";

export default function ParameterForm(visible, change) {
  return (
    <>
      <SideSheet title="Parameter" visible={visible} onCancel={change}>
        <p>This is the content of a basic sidesheet.</p>
        <p>Here is more content...</p>
      </SideSheet>
    </>
  );
}
