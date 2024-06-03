export default eventHandler(async (event) => {
  const { files } = event.context.formidable;
  console.log(files);
  return {
    ok: true,
  };
});
