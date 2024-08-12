import i18next from "i18next";

export const loadNamespaces = async (namespaces) => {
  if (!Array.isArray(namespaces)) {
    namespaces = [namespaces];
  }

  const promises = namespaces.map((ns) =>
    i18next.loadNamespaces(ns).catch((error) => {
      console.warn(`Failed to load namespace: ${ns}`, error);
      return null;
    })
  );

  await Promise.all(promises);
};
